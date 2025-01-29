import {Route} from '@angular/router';
import {OrderComponent} from './order.component';
import {OrderDetailedComponent} from './order-detailed/order-detailed.component';

export const orderRoute: Route[] = [
  {
    path: '',
    component: OrderComponent,
  },
  {
    path: ':id',
    component: OrderDetailedComponent,
  },
];
