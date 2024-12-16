import {CommonModule} from '@angular/common';
import {Component, inject} from '@angular/core';
import {CartService} from '@core/services/cart.service';

@Component({
  standalone: true,
  selector: 'app-summary',
  templateUrl: './order-summary.component.html',
  imports: [CommonModule],
})
export class OrderSummaryComponent {
  cartService = inject(CartService);
}
