import {createActionGroup, props} from '@ngrx/store';
import {GetProductsRequestInterface} from '../types/getProductsRequest.interface';
import {ErrorResponseInterface} from '@shared/models/errors.interface';
import {Pagination} from '@shared/models/pagination.interface';
import {Product} from '@shared/models/product.interface';

export const productActions = createActionGroup({
  source: '[Products]',
  events: {
    'Get Products List': props<{request: GetProductsRequestInterface}>(),
    'Get Products List Success': props<{result: Pagination<Product[]>}>(),
    'Get Products List Failure': props<{errors: ErrorResponseInterface}>(),
  },
});
