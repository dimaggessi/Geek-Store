import {HttpClient} from '@angular/common/http';
import {inject, Injectable} from '@angular/core';
import {environment} from '@environments/environment';
import {AddressInterface} from '@shared/models/address.interface';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AddressService {
  http = inject(HttpClient);

  getAddress(): Observable<AddressInterface> {
    const url = environment.apiUrl + '/address';

    return this.http.get<AddressInterface>(url, {withCredentials: true});
  }

  createOrUpdateAddress(address: AddressInterface) {
    const url = environment.apiUrl + '/address';

    return this.http.post<AddressInterface>(url, address, {withCredentials: true});
  }
}
