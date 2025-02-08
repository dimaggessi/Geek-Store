import {HttpClient} from '@angular/common/http';
import {inject, Injectable} from '@angular/core';
import {environment} from '@environments/environment';
import {CreateOrderDto, OrderInterface} from '@shared/models/order.interface';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  url = `${environment.apiUrl}/orders`;
  private http = inject(HttpClient);
  orderComplete: boolean = false;

  createOrder(createOrderDto: CreateOrderDto) {
    return this.http.post<OrderInterface>(this.url, createOrderDto, {withCredentials: true});
  }

  getUserOrders() {
    return this.http.get<OrderInterface[]>(this.url, {withCredentials: true});
  }

  getOrderDetailed(id: number) {
    return this.http.get<OrderInterface>(this.url + `/${id}`, {withCredentials: true});
  }
}
