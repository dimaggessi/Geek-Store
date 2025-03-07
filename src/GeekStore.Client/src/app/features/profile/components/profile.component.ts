import {CommonModule} from '@angular/common';
import {Component, inject, OnInit} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {RouterLink} from '@angular/router';
import {AddressService} from '@core/services/address.service';
import {ToastService} from '@core/services/toast.service';
import {authActions} from '@features/auth/store/auth.actions';
import {selectUser} from '@features/auth/store/auth.selectors';
import {AuthStateInterface} from '@features/auth/types/authState.interface';
import {Store} from '@ngrx/store';
import {AddressInterface} from '@shared/models/address.interface';
import {UserInterface} from '@shared/models/user.interface';
import {combineLatest, Observable, Subject, take, takeUntil} from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  imports: [CommonModule, FormsModule, RouterLink],
})
export class ProfileComponent implements OnInit {
  private destroy$ = new Subject<void>();
  store = inject(Store<{auth: AuthStateInterface}>);
  addressService = inject(AddressService);
  toastService = inject(ToastService);
  data$!: Observable<{user: UserInterface | null | undefined}>;
  address: AddressInterface = {
    number: '',
    street: '',
    neighborhood: '',
    city: '',
    state: '',
    country: '',
    postalCode: '',
  };

  async ngOnInit() {
    this.addressService.getAddress().subscribe({
      next: (data: AddressInterface) => {
        if (data) {
          this.address = data;
        }
      },
      error: (err) => {
        console.log('Erro ao buscar endereço:', err);
      },
    });

    this.store
      .select(selectUser)
      .pipe(takeUntil(this.destroy$), take(1))
      .subscribe((user) => {
        this.store.dispatch(authActions.getUser());
      });

    this.data$ = combineLatest({
      user: this.store.select(selectUser),
    });
  }

  onSubmit(formValue: AddressInterface): void {
    this.addressService.createOrUpdateAddress(formValue).subscribe({
      next: (response) => {
        console.log('Endereço enviado com sucesso:', response);
      },
      error: (err) => {
        this.toastService.show({
          message: `Erro ao enviar endereço.`,
          type: 'error',
          classname: 'bg-danger text-white text-center',
        });
        console.error('Erro ao enviar o endereço:', err);
      },
    });
    console.log('Endereço enviado:', formValue);
  }

  onInput(event: Event): void {
    const input = event.target as HTMLInputElement;
    input.value = input.value.replace(/\D/g, '');
  }

  ngOnDestroy(): void {
    this.destroy$.next(), this.destroy$.complete();
  }
}
