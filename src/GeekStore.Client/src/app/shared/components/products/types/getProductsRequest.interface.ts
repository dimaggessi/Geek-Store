export interface GetProductsRequestInterface {
  search?: string;
  sort?: string;
  maxPrice?: number;
  minPrice?: number;
  pageIndex?: number;
  pageSize?: number;
  brands?: string[];
  types?: string[];
}
