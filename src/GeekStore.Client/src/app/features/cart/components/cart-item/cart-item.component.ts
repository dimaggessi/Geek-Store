import {CommonModule} from '@angular/common';
import {Component, inject, input, OnChanges, SimpleChanges} from '@angular/core';
import {RouterModule} from '@angular/router';
import {CartService} from '@core/services/cart.service';
import {CartItemInterface} from '@shared/models/cart.interface';
import {debounceTime, distinct, distinctUntilChanged, of, Subject, switchMap} from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-cart-item',
  templateUrl: './cart-item.component.html',
  imports: [CommonModule, RouterModule],
})
export class CartItemComponent {
  item = input.required<CartItemInterface>();
  cartService = inject(CartService);
  quantityChange$ = new Subject<number>();

  incrementQuantity() {
    this.cartService.addItem(this.item());
  }

  decrementQuantity() {
    this.cartService.removeItem(this.item().productId);
  }

  removeItem() {
    this.cartService.removeItem(this.item().productId, this.item().quantity);
  }
}
