import {CommonModule} from '@angular/common';
import {Component, inject, OnInit, AfterViewInit} from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import {RouterLink} from '@angular/router';
import {authActions} from '@features/auth/store/auth.actions';
import {
  selectApiErrors,
  selectIsSubmitting,
} from '@features/auth/store/auth.selectors';
import {AuthStateInterface} from '@features/auth/types/authState.interface';
import {LoginRequestInterface} from '@features/auth/types/loginRequest.interface';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {Store} from '@ngrx/store';
import {ErrorModalComponent} from '@shared/components/error/error-modal.component';
import {
  ErrorInterface,
  ErrorResponseInterface,
  ValidationErrorInterface,
} from '@shared/types/errors.interface';
import {BehaviorSubject, combineLatest, debounceTime, Observable} from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
  imports: [ReactiveFormsModule, CommonModule, RouterLink],
})
export class LoginComponent implements OnInit, AfterViewInit {
  modalService = inject(NgbModal);
  store = inject(Store<{auth: AuthStateInterface}>);
  fb = inject(FormBuilder);
  form!: FormGroup;
  data$!: Observable<{
    isSubmitting: boolean;
    errors: ErrorResponseInterface | null;
  }>;
  private errorsSubject = new BehaviorSubject<ErrorResponseInterface | null>(
    null
  );

  ngAfterViewInit(): void {
    this.errorsSubject.pipe(debounceTime(300)).subscribe((errors) => {
      if (errors) {
        this.openErrorModal(errors.error, errors.validationErrors || []);
      }
    });
  }

  ngOnInit(): void {
    this.form = this.fb.nonNullable.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });

    this.data$ = combineLatest({
      isSubmitting: this.store.select(selectIsSubmitting),
      errors: this.store.select(selectApiErrors),
    });

    this.data$.subscribe((data) => {
      if (data.errors) {
        this.errorsSubject.next(data.errors);
      } else {
        this.errorsSubject.next(null);
      }
    });
  }

  get f(): any {
    return this.form.controls;
  }

  onSubmit() {
    console.log('form', this.form.getRawValue());
    const request: LoginRequestInterface = {
      email: this.form.get('email')?.value,
      password: this.form.get('password')?.value,
    };
    this.store.dispatch(authActions.login({request}));
  }

  openErrorModal(
    error: ErrorInterface,
    validationErrors: ValidationErrorInterface[]
  ): void {
    if (!this.modalService.hasOpenModals()) {
      const modalRef = this.modalService.open(ErrorModalComponent, {
        centered: true,
      });
      modalRef.componentInstance.error = error;
      modalRef.componentInstance.validationErrors = validationErrors;
    }
  }

  get getEmail(): string {
    const control = this.f.email;
    return control?.touched && control?.invalid
      ? 'É necessário informar o email.'
      : 'Email';
  }

  get getPassword(): string {
    const control = this.f.password;
    return control?.touched && control?.invalid
      ? 'É necessário informar a senha.'
      : 'Senha';
  }
}
