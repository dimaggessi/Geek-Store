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
  status: OrderStatus;
  paymentIntentId: string;
}

export interface CreateOrderDto {
  customerEmail: string;
  cartId: string;
  deliveryMethodId: number;
  shippingAddress: AddressInterface;
  paymentSummary: PaymentSummaryInterface;
}

export class OrderParams {
  pageIndex: number = 1;
  pageSize: number = 10;
  totalItems: number = 0;
  status: OrderStatus = OrderStatus.All;
}

export enum OrderStatus {
  Pending,
  PaymentReceived,
  PaymentFailed,
  PaymentMismatch,
  Refunded,
  All,
}
