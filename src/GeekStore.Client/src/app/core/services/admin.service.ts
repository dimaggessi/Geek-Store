import {HttpClient, HttpParams} from '@angular/common/http';
import {Injectable, inject} from '@angular/core';
import {environment} from '@environments/environment';
import {GetProductsResponseInterface} from '@shared/components/products/types/getProductsResponse.interface';
import {OrderInterface, OrderParams, OrderStatus} from '@shared/models/order.interface';
import {Pagination} from '@shared/models/pagination.interface';
import {
  CreateProductInterface,
  ProductInterface,
  ProductParams,
} from '@shared/models/product.interface';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  http = inject(HttpClient);

  getAllOrders(orderParams: OrderParams) {
    const url = `${environment.apiUrl}/admin/orders`;

    let params = new HttpParams();
    if (
      orderParams.status !== undefined &&
      orderParams.status !== null &&
      orderParams.status.valueOf() != OrderStatus.All
    ) {
      params = params.append('status', orderParams.status);
    }
    params = params.append('pageIndex', orderParams.pageIndex);
    params = params.append('pageSize', orderParams.pageSize);

    return this.http.get<Pagination<OrderInterface[]>>(url, {params, withCredentials: true});
  }

  getOrderById(id: number) {
    const url = `${environment.apiUrl}/admin/orders/${id}`;

    return this.http.get<OrderInterface>(url, {withCredentials: true});
  }

  getOrdersByEmail(email: string) {
    const url = `${environment.apiUrl}/admin/orders/${email}`;

    return this.http.get<Pagination<OrderInterface>>(url, {withCredentials: true});
  }

  refundOrder(id: number) {
    const url = `${environment.apiUrl}/admin/orders/refund/${id}`;

    return this.http.post<OrderInterface>(url, {}, {withCredentials: true});
  }

  getAllProducts(productParams: ProductParams) {
    const url = `${environment.apiUrl}/products`;

    let params = new HttpParams();
    if (
      productParams.search !== undefined &&
      productParams.search !== null &&
      productParams.search !== ''
    ) {
      params = params.append('search', productParams.search);
    }
    if (productParams.pageIndex) params = params.append('pageIndex', productParams.pageIndex);
    if (productParams.pageSize) params = params.append('pageSize', productParams.pageSize);

    return this.http.get<GetProductsResponseInterface>(url, {params, withCredentials: true});
  }

  createProduct(product: CreateProductInterface) {
    const url = `${environment.apiUrl}/products`;

    return this.http.post<ProductInterface>(url, {product}, {withCredentials: true});
  }

  deleteProduct(id: number) {
    const url = `${environment.apiUrl}/products/${id}`;

    return this.http.delete(url, {withCredentials: true});
  }
}
