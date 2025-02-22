export interface ProductInterface {
  id: number;
  name: string;
  description: string;
  picture: string;
  type: string;
  brand: string;
  quantity: number;
  price: number;
}

export interface CreateProductInterface {
  name: string;
  description: string;
  picture: string;
  type: string;
  brand: string;
  quantity: number;
  price: number;
  width: number;
  height: number;
  length: number;
  weight: number;
}

export class ProductParams {
  pageIndex: number = 1;
  pageSize: number = 10;
  totalItems: number = 0;
  search?: string = '';
}
