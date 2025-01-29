import {Route} from '@angular/router';
import {CheckoutComponent} from './components/checkout.component';
import {CheckoutSuccessComponent} from './components/checkout-success/checkout-success.component';

export const checkoutRoute: Route[] = [
  {
    path: '',
    component: CheckoutComponent,
  },
  {
    path: 'pedido-confirmado',
    component: CheckoutSuccessComponent,
  },
];
