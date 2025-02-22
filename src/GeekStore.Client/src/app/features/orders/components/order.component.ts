import {Component, inject, OnDestroy, OnInit, Output} from '@angular/core';
import {OrderService} from '@core/services/order.service';
import {OrderInterface} from '@shared/models/order.interface';
import {RouterLink} from '@angular/router';
import {CommonModule} from '@angular/common';
import {Subject, takeUntil} from 'rxjs';
import {StatusPipe} from '@shared/pipes/status.pipe';

@Component({
  selector: 'app-order',
  standalone: true,
  imports: [RouterLink, CommonModule, StatusPipe],
  templateUrl: './order.component.html',
  styleUrl: './order.component.scss',
})
export class OrderComponent implements OnInit, OnDestroy {
  private orderService = inject(OrderService);
  private destroy$ = new Subject<void>();
  orders: OrderInterface[] = [];
  loading: boolean = true;

  ngOnInit(): void {
    this.loadOrders();
  }

  private loadOrders() {
    this.orderService
      .getUserOrders()
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (orders) => ((this.orders = orders), (this.loading = false)),
        error: (err) => (console.error('Erro ao carregar pedidos:', err), (this.loading = false)),
      });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
