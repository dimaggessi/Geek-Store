import {BrandsInterface} from '@shared/models/brands.interface';
import {ErrorResponseInterface} from '@shared/models/errors.interface';
import {Pagination} from '@shared/models/pagination.interface';
import {Product} from '@shared/models/product.interface';
import {TypesInterface} from '@shared/models/types.interface';

export interface ProductStateInterface {
  isSubmitting: boolean;
  isLoading: boolean;
  product: Product | null;
  products: Pagination<Product[]> | null;
  errors: ErrorResponseInterface | null;
  brands: BrandsInterface | null;
  types: TypesInterface | null;
}
