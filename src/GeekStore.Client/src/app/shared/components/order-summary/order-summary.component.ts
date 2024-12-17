import {CommonModule} from '@angular/common';
import {Component, inject} from '@angular/core';
import {RouterLink} from '@angular/router';
import {CartService} from '@core/services/cart.service';

@Component({
  standalone: true,
  selector: 'app-summary',
  templateUrl: './order-summary.component.html',
  imports: [CommonModule, RouterLink],
})
export class OrderSummaryComponent {
  cartService = inject(CartService);
}
