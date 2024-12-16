import {InitializerService} from './../../../core/services/initializer.service';
import {CommonModule} from '@angular/common';
import {Component, inject, input, OnInit} from '@angular/core';
import {CartService} from '@core/services/cart.service';
import {CartItemComponent} from './cart-item/cart-item.component';
import {OrderSummaryComponent} from '../../../shared/components/order-summary/order-summary.component';

@Component({
  standalone: true,
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.scss',
  imports: [CommonModule, CartItemComponent, OrderSummaryComponent],
})
export class CartComponent implements OnInit {
  initializerService = inject(InitializerService);
  cartService = inject(CartService);
  item = input;
  cart = this.cartService.cart();

  ngOnInit(): void {
    this.initializerService.init();
  }
}
