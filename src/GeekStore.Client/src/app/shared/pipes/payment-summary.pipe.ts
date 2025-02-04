import {Pipe, PipeTransform} from '@angular/core';
import {PaymentSummaryInterface} from '@shared/models/payment-summary.interface';

@Pipe({
  name: 'paymentSummary',
  standalone: true,
})
export class PaymentSummaryPipe implements PipeTransform {
  transform(value: PaymentSummaryInterface, ...args: unknown[]): unknown {
    return `${value.brand.toUpperCase()}, **** **** **** ${value.cardLast4}, ${value.expMonth}/${
      value.expYear
    }`;
  }
}
