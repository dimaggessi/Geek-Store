import {AfterViewInit, Component, Inject, OnInit} from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import {Store} from '@ngrx/store';
import {authActions} from '@features/auth/store/auth.actions';
import {RegisterRequestInterface} from '@features/auth/types/registerRequest.interface';
import {
  selectApiErrors,
  selectIsSubmitting,
} from '@features/auth/store/auth.selectors';
import {AuthStateInterface} from '@features/auth/types/authState.interface';
import {BehaviorSubject, combineLatest, debounceTime, Observable} from 'rxjs';
import {CommonModule} from '@angular/common';
import {
  ErrorInterface,
  ErrorResponseInterface,
  ValidationErrorInterface,
} from '@shared/models/errors.interface';
import {ErrorModalComponent} from '@shared/components/error/error-modal.component';
import {NgbModal, NgbModalModule} from '@ng-bootstrap/ng-bootstrap';

@Component({
  standalone: true,
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
  imports: [
    ReactiveFormsModule,
    CommonModule,
    NgbModalModule,
    ErrorModalComponent,
  ],
})
export class RegisterComponent implements OnInit, AfterViewInit {
  form!: FormGroup;
  data$!: Observable<{
    isSubmitting: boolean;
    errors: ErrorResponseInterface | null;
  }>;
  private errorsSubject = new BehaviorSubject<ErrorResponseInterface | null>(
    null
  );

  get f(): any {
    return this.form.controls;
  }

  constructor(
    @Inject(Store) private store: Store<{auth: AuthStateInterface}>,
    @Inject(FormBuilder) private fb: FormBuilder,
    @Inject(NgbModal) private modalService: NgbModal
  ) {}

  ngAfterViewInit(): void {
    this.errorsSubject.pipe(debounceTime(300)).subscribe((errors) => {
      if (errors) {
        this.openErrorModal(errors.error, errors.validationErrors || []);
      }
    });
  }

  ngOnInit(): void {
    this.form = this.fb.nonNullable.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', Validators.required],
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

  onSubmit() {
    console.log('form', this.form.getRawValue());
    const request: RegisterRequestInterface = {
      firstName: this.form.get('firstName')?.value,
      lastName: this.form.get('lastName')?.value,
      email: this.form.get('email')?.value,
      password: this.form.get('password')?.value,
    };
    this.store.dispatch(authActions.register({request}));
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

  get getFirstName(): string {
    const control = this.f.firstName;
    return control?.touched && control?.invalid
      ? 'O nome é obrigatório.'
      : 'Nome';
  }

  get getLastName(): string {
    const control = this.f.lastName;
    return control?.touched && control?.invalid
      ? 'O sobrenome é obrigatório.'
      : 'Sobrenome';
  }

  get getEmail(): string {
    const control = this.f.email;
    return control?.touched && control?.invalid
      ? 'O email é obrigatório.'
      : 'Email';
  }

  get getPassword(): string {
    const control = this.f.password;
    return control?.touched && control?.invalid
      ? 'A senha é obrigatória.'
      : 'Senha';
  }
}
