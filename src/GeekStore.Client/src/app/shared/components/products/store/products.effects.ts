import {inject} from '@angular/core';
import {ProductService} from '@shared/components/products/services/product.service';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {productActions} from './products.actions';
import {catchError, map, of, switchMap} from 'rxjs';
import {Pagination} from '@shared/models/pagination.interface';
import {Product} from '@shared/models/product.interface';
import {HttpErrorResponse} from '@angular/common/http';

export const getProductsEffect = createEffect(
  (actions$ = inject(Actions), productService = inject(ProductService)) => {
    return actions$.pipe(
      ofType(productActions.getProductsList),
      switchMap(({request}) => {
        return productService.getProducts(request).pipe(
          map((pagination: Pagination<Product[]>) => {
            return productActions.getProductsListSuccess({result: pagination});
          }),
          catchError((errorResponse: HttpErrorResponse) => {
            return of(productActions.getProductsListFailure({errors: errorResponse.error}));
          })
        );
      })
    );
  },
  {functional: true}
);
