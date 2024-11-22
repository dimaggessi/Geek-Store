import {CommonModule} from '@angular/common';
import {Component, Input} from '@angular/core';
import {NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';
import {
  ErrorInterface,
  ValidationErrorInterface,
} from '@shared/models/errors.interface';

@Component({
  standalone: true,
  selector: 'app-error-modal',
  templateUrl: './error-modal.component.html',
  imports: [CommonModule],
})
export class ErrorModalComponent {
  @Input() error: ErrorInterface = {} as ErrorInterface;
  @Input() validationErrors: ValidationErrorInterface[] | null = [];

  constructor(public activeModal: NgbActiveModal) {}
}
