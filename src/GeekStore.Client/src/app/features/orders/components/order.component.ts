import {Component, inject, OnInit} from '@angular/core';
import {OrderService} from '@core/services/order.service';
import {OrderInterface} from '@shared/models/order.interface';

@Component({
  selector: 'app-order',
  standalone: true,
  imports: [],
  templateUrl: './order.component.html',
  styleUrl: './order.component.scss',
})
export class OrderComponent implements OnInit {
  private orderService = inject(OrderService);
  orders: OrderInterface[] = [];

  ngOnInit(): void {
    this.orderService.getUserOrders().subscribe({
      next: (orders) => (this.orders = orders),
    });
  }
}
