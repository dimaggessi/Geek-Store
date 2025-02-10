import {CommonModule} from '@angular/common';
import {
  AfterViewChecked,
  ChangeDetectorRef,
  Component,
  HostListener,
  inject,
  OnChanges,
  OnInit,
  signal,
} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {AddressService} from '@core/services/address.service';
import {CartService} from '@core/services/cart.service';
import {ShippingService} from '@core/services/shipping.service';
import {StripeService} from '@core/services/stripe.service';
import {ToastService} from '@core/services/toast.service';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {AddressInterface} from '@shared/models/address.interface';
import {Cart} from '@shared/models/cart.interface';
import {DeliveryInterface} from '@shared/models/delivery.interface';
import {ConfirmationToken, StripePaymentElement} from '@stripe/stripe-js';
import {PaymentCardPipe} from '@shared/pipes/payment-card.pipe';
import {Router} from '@angular/router';
import {CreateOrderDto} from '@shared/models/order.interface';
import {firstValueFrom} from 'rxjs';
import {OrderService} from '@core/services/order.service';

@Component({
  standalone: true,
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  imports: [CommonModule, FormsModule, PaymentCardPipe],
})
export class CheckoutComponent implements OnInit, AfterViewChecked, OnChanges {
  private router = inject(Router);
  private cdr = inject(ChangeDetectorRef);
  private stripeService = inject(StripeService);
  orderService = inject(OrderService);
  cartService = inject(CartService);
  toastService = inject(ToastService);
  modalService = inject(NgbModal);
  addressService = inject(AddressService);
  shippingService = inject(ShippingService);
  paymentElement?: StripePaymentElement;
  paymentElementError = signal<boolean>(false);
  confirmationToken?: ConfirmationToken;
  currentStep: number = 1;
  addressFilled: boolean = false;
  loading: boolean = false;
  address: AddressInterface = {
    number: '',
    street: '',
    neighborhood: '',
    city: '',
    state: '',
    country: '',
    postalCode: '',
  };
  selectedDeliveryMethod: number = 1;
  deliveryResult?: DeliveryInterface;
  hasScrollDown = false;

  async ngOnInit() {
    this.addressService.getAddress().subscribe({
      next: (data: AddressInterface) => {
        if (data) {
          this.address = data;
          this.addressFilled = true;
          this.calculateDelivery(this.address.postalCode, this.selectedDeliveryMethod);
        }
      },
      error: (err) => {
        console.log('Erro ao buscar endereço:', err);
      },
    });
  }

  ngOnChanges(): void {
    if (this.paymentElementError()) {
      setTimeout(() => {
        this.previousStep();
      }, 2000);
    }
  }

  ngAfterViewChecked() {
    this.checkScroll();
    this.cdr.detectChanges();
  }

  checkScroll() {
    this.hasScrollDown =
      document.documentElement.scrollHeight > window.innerHeight + window.scrollY;
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: Event) {
    this.checkScroll();
  }

  scrollDown() {
    window.scrollTo({
      top: document.documentElement.scrollHeight,
      behavior: 'smooth',
    });
    this.checkScroll();
  }

  onSubmit(formValue: AddressInterface): void {
    this.addressService.createOrUpdateAddress(formValue).subscribe({
      next: (response) => {
        console.log('Endereço enviado com sucesso:', response);
      },
      error: (err) => {
        console.error('Erro ao enviar o endereço:', err);
      },
    });
    console.log('Endereço enviado:', formValue);
  }

  onInput(event: Event): void {
    const input = event.target as HTMLInputElement;
    input.value = input.value.replace(/\D/g, '');

    if (input.value.length === 8) {
      this.calculateDelivery(input.value, this.selectedDeliveryMethod);
    }
  }

  onDeliveryMethodChange(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.selectedDeliveryMethod = Number(input.value);
    this.calculateDelivery(this.address.postalCode, this.selectedDeliveryMethod);
  }

  calculateDelivery(postalCode: string, deliveryMethod: number) {
    if (!postalCode || postalCode.length !== 8) {
      this.toastService.show({
        message: 'Inserir um CEP válido com 8 dígitos numéricos!',
        type: 'error',
        classname: 'bg-danger text-white text-center',
      });
      return;
    }

    if (this.cartService.cart() === null) {
      this.toastService.show({
        message: 'O carrinho não pode estar vazio para calcular o Frete!',
        type: 'info',
        classname: 'bg-success text-white text-center',
      });
      return;
    }

    this.shippingService
      .shippingCalculator(this.cartService.cart()!.id, deliveryMethod, postalCode)
      .subscribe({
        next: (response: DeliveryInterface[]) => {
          const currentCart = this.cartService.cart();
          if (currentCart) {
            this.cartService.shippingCost = response[0].price;
            this.cartService.deliveryTime = response[0].deliveryTimeInDays;

            const updatedCart: Cart = {
              ...currentCart,
              deliveryMethodId: deliveryMethod,
              postalCode: postalCode,
            };

            this.cartService.setCart(updatedCart);
          }
        },
        error: (err) => {
          this.toastService.show({
            message: 'Erro ao calcular o frete. Tente novamente mais tarde.',
            type: 'error',
            classname: 'bg-danger text-white text-center',
          });
          console.error('Erro ao calcular o frete:', err);
        },
      });
  }

  async getConfirmationToken() {
    if (!this.paymentElement) {
      console.error('Elemento de pagamento não encontrado. Tente novamente.');
      return;
    }
    try {
      const result = await this.stripeService.createConfirmationToken();
      if (result.error) throw new Error(result.error.message);
      this.confirmationToken = result.confirmationToken;
      console.log('Confirmation Token de Pagamento:', this.confirmationToken);
    } catch (error: any) {
      this.previousStep();
      this.toastService.show({
        message: 'Falha ao criar Token de Pagamento. Contate o suporte.',
        type: 'error',
        classname: 'bg-danger text-white text-center',
      });
      console.log(error.message);
    }
  }

  async confirmPayment() {
    this.loading = true;
    try {
      if (this.confirmationToken) {
        const order = this.createOrderModel();
        const orderResult = await firstValueFrom(this.orderService.createOrder(order));

        // Se o pagamento já foi confirmado
        const paymentIntentResult = await this.stripeService.retrievePaymentIntentStatus();

        if (paymentIntentResult?.paymentIntent?.status === 'succeeded') {
          if (orderResult) {
            this.orderService.orderComplete = true;
            this.cartService.deleteCart();
            this.toastService.show({
              message: 'Pedido já foi processado com sucesso.',
              type: 'success',
              classname: 'bg-success text-white text-center',
            });
            this.router.navigateByUrl('/checkout/pedido-confirmado');
            return;
          } else {
            this.toastService.show({
              message: 'Falha ao criar o pedido. Contate o suporte imediatamente.',
              type: 'error',
              classname: 'bg-danger text-white text-center',
            });
            this.loading = false;
            throw new Error('Falha ao criar pedido na base de dados.');
          }
        }

        // Caso o pagamento não esteja confirmado
        const result = await this.stripeService.confirmPayment(this.confirmationToken);

        if (result.paymentIntent?.status === 'succeeded') {
          if (orderResult) {
            this.orderService.orderComplete = true;
            this.cartService.deleteCart();
            this.toastService.show({
              message: 'Pedido realizado com sucesso.',
              type: 'success',
              classname: 'bg-success text-white text-center',
            });
            this.router.navigateByUrl('/checkout/pedido-confirmado');
          } else {
            this.toastService.show({
              message: 'Falha ao criar o pedido. Contate o suporte imediatamente.',
              type: 'error',
              classname: 'bg-danger text-white text-center',
            });
            this.loading = false;
            throw new Error('Falha ao criar pedido na base de dados.');
          }
        }

        if (result.error) {
          this.toastService.show({
            message: 'Erro ao processar o pagamento',
            type: 'error',
            classname: 'bg-danger text-white text-center',
          });
          console.log('Erro ao processar o pagamento:', result.error.message);
          this.loading = false;
        }
      }
    } catch (error: any) {
      this.toastService.show({
        message: 'Não foi possível processar o pedido. Tente novamente em alguns instantes.',
        type: 'error',
        classname: 'bg-danger text-white text-center',
      });
      console.error('Erro ao processar pedido:', error);
      this.loading = false;
    }
  }

  async nextStep() {
    if (this.currentStep === 2) {
      if (this.paymentElement) {
        try {
          await this.getConfirmationToken();
        } catch (error: any) {
          this.toastService.show({
            message: 'Ocorreu um erro ao tentar receber o Token de Pagamento.',
            type: 'error',
            classname: 'bg-danger text-white text-center',
          });
          return;
        }
      }
    }

    if (this.currentStep < 3) {
      this.currentStep++;
    }

    this.checkScroll();

    if (this.currentStep === 2) {
      await this.renderPaymentElement();
    }
  }

  previousStep() {
    if (this.currentStep > 1) {
      this.currentStep--;
    }
    this.checkScroll();
  }

  private createOrderModel(): CreateOrderDto {
    const userEmail = this.getUserEmail();
    const cart = this.cartService.cart();
    let orderAddress = this.address;
    orderAddress.name = String(userEmail);
    const card = this.confirmationToken?.payment_method_preview.card;

    if (!cart?.id || !cart?.deliveryMethodId || !orderAddress || !card || !userEmail) {
      this.toastService.show({
        message: 'Não foi possível processar o pedido. Tente novamente em alguns instantes.',
        type: 'error',
        classname: 'bg-danger text-white text-center',
      });
      throw new Error(
        `Erro ao criar o pedido. CartdId: ${cart?.id}, DeliveryMethodId: ${cart?.deliveryMethodId}, 
				OrderAddress: ${orderAddress}, UserEmail: ${userEmail}, Card: ${card}`
      );
    }

    return {
      customerEmail: userEmail,
      cartId: cart.id,
      deliveryMethodId: cart.deliveryMethodId,
      shippingAddress: orderAddress,
      paymentSummary: {
        cardLast4: Number(card.last4),
        brand: card.brand,
        expMonth: card.exp_month,
        expYear: card.exp_year,
      },
    };
  }

  private async renderPaymentElement(): Promise<void> {
    try {
      // Await a DOM update
      this.cdr.detectChanges();
      // Mount Payment Element
      this.paymentElement = await this.stripeService.createPaymentElement();
      this.paymentElement.mount('#payment-element');

      return new Promise((resolve) => {
        this.paymentElement?.on('ready', () => {
          console.log('Elemento de pagamento montado!');
          resolve();
        });
      });
    } catch (error: any) {
      console.log('Um erro ocorreu ao processar o paymentElement:', error);
      this.paymentElementError.set(true);
    }
  }

  private getUserEmail(): string | null {
    const authData = localStorage.getItem('auth');
    return authData ? JSON.parse(authData).user?.email : null;
  }
}
