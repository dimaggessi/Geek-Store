import {CommonModule} from '@angular/common';
import {Component, inject, OnInit} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {AddressService} from '@core/services/address.service';
import {StripeService} from '@core/services/stripe.service';
import {AddressInterface} from '@shared/models/address.interface';
import {StripeAddressElement} from '@stripe/stripe-js';

@Component({
  standalone: true,
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  imports: [CommonModule, FormsModule],
})
export class CheckoutComponent implements OnInit {
  private stripeService = inject(StripeService);
  addressService = inject(AddressService);
  addressElement?: StripeAddressElement;
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

  ngOnInit() {
    this.addressService.getAddress().subscribe({
      next: (data: AddressInterface) => {
        if (data) {
          this.address = data;
          this.addressFilled = true;
        }
      },
      error: (err) => {
        console.log('Erro ao buscar endereço:', err);
      },
    });
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

  nextStep() {
    if (this.currentStep < 3) {
      this.currentStep++;
    }
  }

  previousStep() {
    if (this.currentStep > 1) {
      this.currentStep--;
    }
  }
}
