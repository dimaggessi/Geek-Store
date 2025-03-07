import {inject, Injectable} from '@angular/core';
import {CartService} from './cart.service';
import {catchError, of} from 'rxjs';

@Injectable({providedIn: 'root'})
export class InitializerService {
  private cartService = inject(CartService);

  init() {
    const cartId = localStorage.getItem('cart_id');
    if (!cartId) return of(null);

    return this.cartService.getCart(cartId).pipe(
      catchError((error) => {
        console.warn('Erro ao inicializar carrinho:', error);
        return of(null);
      })
    );
  }
}
