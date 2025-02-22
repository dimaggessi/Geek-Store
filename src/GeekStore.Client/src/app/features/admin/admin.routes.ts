import {Route} from '@angular/router';
import {AdminComponent} from './components/admin.component';
import {AdminOrderDetailsComponent} from './components/admin-order-details/admin-order-details.component';

export const adminRoute: Route[] = [
  {
    path: '',
    component: AdminComponent,
  },
  {
    path: 'pedidos/:id',
    component: AdminOrderDetailsComponent,
  },
];
