import {ErrorResponseInterface} from '@shared/models/errors.interface';
import {Pagination} from '@shared/models/pagination.interface';
import {Product} from '@shared/models/product.interface';

export interface ProductStateInterface {
  isSubmitting: boolean;
  isLoading: boolean;
  data: Pagination<Product[]> | null;
  errors: ErrorResponseInterface | null;
}
