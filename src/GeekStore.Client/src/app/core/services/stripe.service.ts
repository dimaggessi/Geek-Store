import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {inject, Injectable} from '@angular/core';
import {environment} from '@environments/environment';
import {
  ConfirmationToken,
  loadStripe,
  Stripe,
  StripeElements,
  StripePaymentElement,
} from '@stripe/stripe-js';
import {CartService} from './cart.service';
import {Cart} from '@shared/models/cart.interface';
import {catchError, firstValueFrom, map, throwError} from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class StripeService {
  private cartService = inject(CartService);
  private http = inject(HttpClient);
  private stripePromise?: Promise<Stripe | null>;
  private elements?: StripeElements; // used to store address or payment cards
  private paymentElement?: StripePaymentElement;

  constructor() {
    this.stripePromise = loadStripe(environment.stripePublicKey);
  }

  async getStripeInstance() {
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

  async createPaymentElement() {
    if (!this.paymentElement) {
      const elements = await this.initializeElements();
      if (elements) {
        this.paymentElement = elements.create('payment');
      } else {
        throw new Error('A instância de elements não pode ser inicializada.');
      }
    }
    return this.paymentElement;
  }

  async createConfirmationToken() {
    const stripe = await this.getStripeInstance();
    const elements = await this.initializeElements();
    const result = await elements.submit();
    if (result.error) throw new Error(result.error.message);

    if (stripe) {
      return await stripe.createConfirmationToken({elements});
    } else {
      throw new Error('O método de pagamento Stripe não está acessível.');
    }
  }

  async confirmPayment(confirmationToken: ConfirmationToken) {
    const stripe = await this.getStripeInstance();
    const elements = await this.initializeElements();
    const result = await elements.submit();
    if (result.error) throw new Error(result.error.message);

    const clientSecret = this.cartService.cart()?.clientSecret;

    if (stripe && clientSecret) {
      return await stripe.confirmPayment({
        clientSecret: clientSecret,
        confirmParams: {confirmation_token: confirmationToken.id},
        redirect: 'if_required',
      });
    } else {
      throw new Error('Não foi possível carregar o Stripe.');
    }
  }

  createOrUpdatePaymentIntent() {
    const url = environment.apiUrl;
    const cart = this.cartService.cart();

    if (!cart) throw new Error('Há um problema com o carrinho.');

    return this.http.post<Cart>(url + '/payments/' + cart.id, {}).pipe(
      map((cart) => {
        this.cartService.cart.set(cart);
        return cart;
      }),
      catchError((error: HttpErrorResponse) => {
        const errorMessage = error?.error?.detail;
        return throwError(() => new Error(errorMessage));
      })
    );
  }

  disposeElements() {
    this.elements = undefined;
    this.paymentElement = undefined;
  }
}
