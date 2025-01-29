import {AddressInterface} from './address.interface';
import {CartItemInterface} from './cart.interface';
import {PaymentSummaryInterface} from './payment-summary.interface';

export interface OrderInterface {
  id: number;
  customerEmail: string;
  orderDate: string;
  shippingAddress: AddressInterface;
  deliveryMethodName: string;
  deliveryTimeInDays: number;
  shippingPrice: number;
  paymentSummary: PaymentSummaryInterface;
  orderItems: CartItemInterface[];
  subTotal: number;
  total: number;
  status: string;
  paymentIntentId: string;
}

export interface CreateOrderDto {
  customerEmail: string;
  cartId: string;
  deliveryMethodId: number;
  shippingAddress: AddressInterface;
  paymentSummary: PaymentSummaryInterface;
}
