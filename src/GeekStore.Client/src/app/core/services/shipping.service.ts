import {HttpClient} from '@angular/common/http';
import {inject, Injectable} from '@angular/core';
import {environment} from '@environments/environment';
import {CartItemInterface} from '@shared/models/cart.interface';
import {DeliveryInterface} from '@shared/models/delivery.interface';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ShippingService {
  http = inject(HttpClient);

  shippingCalculator(
    cartId: string,
    deliveryMethodId: number,
    postalCode: string
  ): Observable<DeliveryInterface[]> {
    const url = environment.apiUrl;

    return this.http.post<DeliveryInterface[]>(url + '/delivery/', {
      cartId,
      deliveryMethodId,
      postalCode,
    });
  }
}
