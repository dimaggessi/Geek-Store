import {Pagination} from '@shared/models/pagination.interface';
import {ProductInterface} from '@shared/models/product.interface';

export interface GetProductsResponseInterface {
  response: Pagination<ProductInterface[]>;
}
