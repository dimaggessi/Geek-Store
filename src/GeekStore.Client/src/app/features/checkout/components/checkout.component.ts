import {CommonModule} from '@angular/common';
import {
  AfterViewChecked,
  ChangeDetectorRef,
  Component,
  HostListener,
  inject,
  OnInit,
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

@Component({
  standalone: true,
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  imports: [CommonModule, FormsModule, PaymentCardPipe],
})
export class CheckoutComponent implements OnInit, AfterViewChecked {
  private router = inject(Router);
  private cdr = inject(ChangeDetectorRef);
  private stripeService = inject(StripeService);
  cartService = inject(CartService);
  toastService = inject(ToastService);
  modalService = inject(NgbModal);
  addressService = inject(AddressService);
  shippingService = inject(ShippingService);
  paymentElement?: StripePaymentElement;
  confirmationToken?: ConfirmationToken;
  currentStep: number = 1;
  addressFilled: boolean = false;
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
        message: error.message,
        type: 'error',
        classname: 'bg-danger text-white text-center',
      });
      console.log(error.message);
    }
  }

  async confirmPayment() {
    try {
      if (this.confirmationToken) {
        const result = await this.stripeService.confirmPayment(this.confirmationToken);
        if (result.error) {
          this.toastService.show({
            message: result.error.message,
            type: 'error',
            classname: 'bg-danger text-white text-center',
          });
          this.previousStep();
        } else {
          this.cartService.deleteCart();
          this.toastService.show({
            message: 'Pagamento realizado com sucesso.',
            type: 'success',
            classname: 'bg-success text-white text-center',
          });
          this.router.navigateByUrl('/checkout/pedido-confirmado');
        }
      }
    } catch (error: any) {
      this.toastService.show({
        message: 'Não foi possível processar o pagamento. Tente novamente em alguns instantes.',
        type: 'error',
        classname: 'bg-danger text-white text-center',
      });
      console.error('Erro ao processar pagamento:', error);
    }
  }

  async nextStep() {
    if (this.currentStep === 2) {
      if (this.paymentElement) {
        await this.getConfirmationToken();
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
    }
  }
}
