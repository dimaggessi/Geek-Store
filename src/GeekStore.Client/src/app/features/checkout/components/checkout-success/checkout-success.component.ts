import {CurrencyPipe, DatePipe, CommonModule} from '@angular/common';
import {Component, inject, OnDestroy} from '@angular/core';
import {SignalrService} from '@core/services/signalr.service';
import {PaymentSummaryPipe} from '@shared/pipes/payment-summary.pipe';
import {AddressPipe} from '@shared/pipes/address.pipe';
import {RouterLink} from '@angular/router';
import {OrderService} from '@core/services/order.service';
import {StatusPipe} from '@shared/pipes/status.pipe';

@Component({
  selector: 'app-checkout-success',
  standalone: true,
  imports: [
    CommonModule,
    DatePipe,
    PaymentSummaryPipe,
    AddressPipe,
    CurrencyPipe,
    RouterLink,
    StatusPipe,
  ],
  templateUrl: './checkout-success.component.html',
  styleUrl: './checkout-success.component.scss',
})
export class CheckoutSuccessComponent implements OnDestroy {
  private orderService = inject(OrderService);
  signalRService = inject(SignalrService);

  ngOnDestroy(): void {
    this.orderService.orderComplete = false;
    this.signalRService.orderSignal.set(null);
  }
}
