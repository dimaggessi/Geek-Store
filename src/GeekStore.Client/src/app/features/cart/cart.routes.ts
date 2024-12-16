import {Route} from '@angular/router';
import {CartComponent} from './components/cart.component';

export const cartRoute: Route[] = [
  {
    path: '',
    component: CartComponent,
  },
];
