import {Component, inject, OnInit, Output} from '@angular/core';
import {OrderService} from '@core/services/order.service';
import {OrderInterface} from '@shared/models/order.interface';
import {RouterLink} from '@angular/router';
import {CommonModule} from '@angular/common';

@Component({
  selector: 'app-order',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './order.component.html',
  styleUrl: './order.component.scss',
})
export class OrderComponent implements OnInit {
  private orderService = inject(OrderService);
  orders: OrderInterface[] = [];
  loading: boolean = true;

  ngOnInit(): void {
    this.loadOrders();
  }

  private loadOrders() {
    this.orderService.getUserOrders().subscribe({
      next: (orders) => ((this.orders = orders), (this.loading = false)),
      error: (err) => (console.error('Erro ao carregar pedidos:', err), (this.loading = false)),
    });
  }
}
