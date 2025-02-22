import {CommonModule} from '@angular/common';
import {Component, inject, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute, RouterLink} from '@angular/router';
import {OrderService} from '@core/services/order.service';
import {ToastService} from '@core/services/toast.service';
import {OrderInterface} from '@shared/models/order.interface';
import {AddressPipe} from '@shared/pipes/address.pipe';
import {PaymentSummaryPipe} from '@shared/pipes/payment-summary.pipe';
import {Subject, takeUntil} from 'rxjs';
import {StatusPipe} from '@shared/pipes/status.pipe';

@Component({
  selector: 'app-order-detailed',
  standalone: true,
  imports: [CommonModule, AddressPipe, PaymentSummaryPipe, RouterLink, StatusPipe],
  templateUrl: './order-detailed.component.html',
  styleUrl: './order-detailed.component.scss',
})
export class OrderDetailedComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();
  private orderService = inject(OrderService);
  private activatedRoute = inject(ActivatedRoute);
  toastService = inject(ToastService);
  order?: OrderInterface;

  ngOnInit(): void {
    this.loadOrder();
  }

  loadOrder() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (!id) return;

    this.orderService
      .getOrderDetailed(+id)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (order) => (this.order = order),
        error: (error) => {
          this.toastService.show({
            message: 'Não foi possível carregar os detalhes do pedido',
            type: 'error',
            classname: 'bg-danger text-white text-center',
          });
          console.log('Não foi possível carregar o pedido: ', error);
        },
      });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
