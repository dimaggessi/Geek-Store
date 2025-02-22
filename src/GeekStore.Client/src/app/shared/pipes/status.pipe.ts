import {Pipe, PipeTransform} from '@angular/core';
import {OrderStatus} from '@shared/models/order.interface';

@Pipe({
  name: 'status',
  standalone: true,
})
export class StatusPipe implements PipeTransform {
  private statusMap = new Map<OrderStatus | string, string>([
    ['Pending', 'Pagamento Pendente'],
    ['PaymentReceived', 'Pagamento Recebido'],
    ['PaymentFailed', 'Pagamento Falhou'],
    ['PaymentMismatch', 'Pagamento Divergente'],
    ['Refunded', 'Reembolsado'],
  ]);

  transform(value: OrderStatus | string, ...args: unknown[]): string {
    return this.statusMap.get(value) || 'Desconhecido';
  }
}
