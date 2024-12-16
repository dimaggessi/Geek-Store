import {Routes} from '@angular/router';

export const routes: Routes = [
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
];
