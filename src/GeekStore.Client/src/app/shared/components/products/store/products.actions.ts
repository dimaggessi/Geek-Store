import {ErrorResponseInterface} from '@shared/models/errors.interface';
import {createActionGroup, emptyProps, props} from '@ngrx/store';
import {GetProductsRequestInterface} from '../types/getProductsRequest.interface';
import {Pagination} from '@shared/models/pagination.interface';
import {ProductInterface} from '@shared/models/product.interface';
import {BrandsInterface} from '@shared/models/brands.interface';
import {TypesInterface} from '@shared/models/types.interface';

export const productActions = createActionGroup({
  source: '[Products]',
  events: {
    'Get Products List': props<{request: GetProductsRequestInterface}>(),
    'Get Products List Success': props<{result: Pagination<ProductInterface[]>}>(),
    'Get Products List Failure': props<{errors: ErrorResponseInterface}>(),

    'Get Product By Id': props<{id: number}>(),
    'Get Product By Id Success': props<{product: ProductInterface}>(),
    'Get Product By Id Failure': props<{errors: ErrorResponseInterface}>(),
  },
});

export const brandActions = createActionGroup({
  source: '[Brands]',
  events: {
    'Get Brands List': emptyProps(),
    'Get Brands Success': props<{result: BrandsInterface}>(),
    'Get Brands Failure': props<{errors: ErrorResponseInterface}>(),
  },
});

export const typesActions = createActionGroup({
  source: '[Types]',
  events: {
    'Get Types List': emptyProps(),
    'Get Types Success': props<{result: TypesInterface}>(),
    'Get Types Failure': props<{errors: ErrorResponseInterface}>(),
  },
});
