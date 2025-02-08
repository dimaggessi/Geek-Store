import {CommonModule} from '@angular/common';
import {Component, inject} from '@angular/core';
import {Router, RouterLink} from '@angular/router';
import {CartService} from '@core/services/cart.service';
import {ShippingService} from '@core/services/shipping.service';
import {ToastService} from '@core/services/toast.service';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {DeliveryInterface} from '@shared/models/delivery.interface';
import {ErrorInterface} from '@shared/models/errors.interface';
import {ErrorModalComponent} from '@shared/components/error-modal/error-modal.component';

@Component({
  standalone: true,
  selector: 'app-order-summary',
  templateUrl: './order-summary.component.html',
  imports: [CommonModule, RouterLink],
})
export class OrderSummaryComponent {
  toastService = inject(ToastService);
  cartService = inject(CartService);
  shippingService = inject(ShippingService);
  modalService = inject(NgbModal);
  route = inject(Router);

  selectedDeliveryMethod: number = 1;
  deliveryResult?: DeliveryInterface;

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
        classname: 'bg-info text-white text-center',
      });
      return;
    }

    this.shippingService
      .shippingCalculator(this.cartService.cart()!.id, deliveryMethod, postalCode)
      .subscribe({
        next: (response: DeliveryInterface[]) => {
          this.cartService.shippingCost = response[0].price;
          this.cartService.deliveryTime = response[0].deliveryTimeInDays;
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

  summaryValidator() {
    if (this.cartService.itemCount() === 0) {
      this.toastService.show({
        message: 'O carrinho não pode estar vazio!',
        type: 'error',
        classname: 'bg-danger text-white text-center',
      });
    } else {
      let auth = localStorage.getItem('auth');
      let isLoggedIn = auth ? JSON.parse(auth).isLoggedIn : false;

      if (!isLoggedIn) {
        this.route.navigateByUrl('/entrar?returnUrl=/checkout');
      } else {
        this.route.navigateByUrl('/checkout');
      }
    }
  }

  openPostalCodeModal(error: ErrorInterface): void {
    if (!this.modalService.hasOpenModals()) {
      const modalRef = this.modalService.open(ErrorModalComponent, {
        centered: true,
      });
      modalRef.componentInstance.error = error;
    }
  }
}
