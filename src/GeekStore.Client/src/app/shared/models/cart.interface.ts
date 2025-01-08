import {v4 as uuidv4} from 'uuid';

export interface CartInterface {
  id: string;
  items: CartItemInterface[];
  deliveryMethodId?: number;
  postalCode?: string;
  clientSecret?: string;
  paymentIntentId?: string;
}

export interface CartItemInterface {
  productId: number;
  productName: string;
  price: number;
  quantity: number;
  maxQuantity: number;
  picture: string;
  brand: string;
  type: string;
}

export class Cart implements CartInterface {
  id = uuidv4();
  items: CartItemInterface[] = [];
  deliveryMethodId?: number;
  postalCode?: string;
  clientSecret?: string;
  paymentIntentId?: string;
}
