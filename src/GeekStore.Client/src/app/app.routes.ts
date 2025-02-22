import {Routes} from '@angular/router';
import {adminGuard} from '@core/guards/admin.guard';
import {authGuard} from '@core/guards/auth.guard';
import {HomeComponent} from '@features/home/home.component';

export const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'registro',
    loadChildren: () => import('@features/auth/auth.routes').then((r) => r.registerRoute),
  },
  {
    path: 'entrar',
    loadChildren: () => import('@features/auth/auth.routes').then((r) => r.loginRoute),
  },
  {
    path: 'loja',
    loadChildren: () => import('@features/shop/shop.routes').then((r) => r.shopRoute),
  },
  {
    path: 'loja/:id',
    loadChildren: () => import('@features/shop/shop.routes').then((r) => r.shopDetailsRoute),
  },
  {
    path: 'carrinho',
    loadChildren: () => import('@features/cart/cart.routes').then((r) => r.cartRoute),
  },
  {
    path: 'checkout',
    loadChildren: () => import('@features/checkout/checkout.routes').then((r) => r.checkoutRoute),
    canActivate: [authGuard],
  },
  {
    path: 'pedidos',
    loadChildren: () =>
      import('@features/orders/components/order.routes').then((r) => r.orderRoute),
    canActivate: [authGuard],
  },
  {
    path: 'admin',
    loadChildren: () => import('@features/admin/admin.routes').then((r) => r.adminRoute),
    canActivate: [authGuard, adminGuard],
  },
];
