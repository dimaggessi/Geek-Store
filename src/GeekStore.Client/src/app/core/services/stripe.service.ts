import {HttpClient} from '@angular/common/http';
import {inject, Injectable} from '@angular/core';
import {environment} from '@environments/environment';
import {loadStripe, Stripe, StripeElements} from '@stripe/stripe-js';
import {CartService} from './cart.service';
import {Cart} from '@shared/models/cart.interface';
import {firstValueFrom, map} from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class StripeService {
  private cartService = inject(CartService);
  private http = inject(HttpClient);
  private stripePromise?: Promise<Stripe | null>;
  private elements?: StripeElements; // used to store address or payment cards

  constructor() {
    this.stripePromise = loadStripe(environment.stripePublicKey);
  }

  getStripeInstance() {
    return this.stripePromise;
  }

  async initializeElements() {
    if (!this.elements) {
      const stripe = await this.getStripeInstance();
      if (stripe) {
        const cart = await firstValueFrom(this.createOrUpdatePaymentIntent());
        this.elements = stripe.elements({
          clientSecret: cart.clientSecret,
          appearance: {labels: 'floating'},
        });
      } else {
        throw new Error('Stripe não foi carregado corretamente.');
      }
    }
    return this.elements;
  }

  createOrUpdatePaymentIntent() {
    const url = environment.apiUrl;
    const cart = this.cartService.cart();

    if (!cart) throw new Error('Há um problema com o carrinho.');
    return this.http.post<Cart>(url + '/payments/' + cart.id, {}).pipe(
      map((cart) => {
        this.cartService.cart.set(cart); // signal method: cart set locally
        return cart;
      })
    );
  }
}
