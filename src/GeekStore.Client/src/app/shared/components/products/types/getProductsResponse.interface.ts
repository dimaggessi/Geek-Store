import {Pagination} from '@shared/models/pagination.interface';
import {Product} from '@shared/models/product.interface';

export interface GetProductsResponseInterface {
  response: Pagination<Product[]>;
}
