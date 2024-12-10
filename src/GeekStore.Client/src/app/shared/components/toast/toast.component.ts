import {CommonModule} from '@angular/common';
import {Component, inject} from '@angular/core';
import {ToastService} from '@core/services/toast.service';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';

@Component({
  standalone: true,
  selector: 'app-toast',
  templateUrl: './toast.component.html',
  styleUrl: './toast.component.scss',
  imports: [NgbModule, CommonModule],
})
export class ToastComponent {
  toastService = inject(ToastService);
}
