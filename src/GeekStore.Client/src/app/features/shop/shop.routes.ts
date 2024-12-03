import {Route} from '@angular/router';
import {ShopComponent} from './components/shop.component';
import {ProductDetailsComponent} from '@shared/components/products/components/product-details/product-details.component';

export const shopRoute: Route[] = [
  {
    path: '',
    component: ShopComponent,
  },
];

export const shopDetailsRoute: Route[] = [
  {
    path: '',
    component: ProductDetailsComponent,
  },
];
