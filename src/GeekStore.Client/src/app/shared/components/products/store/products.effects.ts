import {inject} from '@angular/core';
import {ProductService} from '@shared/components/products/services/product.service';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {brandActions, productActions, typesActions} from './products.actions';
import {catchError, map, of, switchMap} from 'rxjs';
import {Pagination} from '@shared/models/pagination.interface';
import {Product} from '@shared/models/product.interface';
import {HttpErrorResponse} from '@angular/common/http';
import {BrandsInterface} from '@shared/models/brands.interface';
import {TypesInterface} from '@shared/models/types.interface';

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

export const getBrandsEffect = createEffect(
  (action$ = inject(Actions), productService = inject(ProductService)) => {
    return action$.pipe(
      ofType(brandActions.getBrandsList),
      switchMap(() => {
        return productService.getBrands().pipe(
          map((response: BrandsInterface) => {
            return brandActions.getBrandsSuccess({result: response});
          }),
          catchError((errorResponse: HttpErrorResponse) => {
            return of(brandActions.getBrandsFailure({errors: errorResponse.error}));
          })
        );
      })
    );
  },
  {functional: true}
);

export const getTypesEffect = createEffect(
  (action$ = inject(Actions), productService = inject(ProductService)) => {
    return action$.pipe(
      ofType(typesActions.getTypesList),
      switchMap(() => {
        return productService.getTypes().pipe(
          map((response: TypesInterface) => {
            return typesActions.getTypesSuccess({result: response});
          }),
          catchError((errorResponse: HttpErrorResponse) => {
            return of(typesActions.getTypesFailure({errors: errorResponse.error}));
          })
        );
      })
    );
  },
  {functional: true}
);
