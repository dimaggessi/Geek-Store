import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Inject, Injectable} from '@angular/core';
import {RegisterRequestInterface} from '../types/registerRequest.interface';
import {map, Observable} from 'rxjs';
import {UserInterface} from '@shared/types/user.interface';
import {environment} from '@environments/environment';
import {AuthResponseInterface} from '../types/authResponse.interface';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(@Inject(HttpClient) private http: HttpClient) {}

  register(data: RegisterRequestInterface): Observable<UserInterface> {
    const url = environment.apiUrl + '/auth/register';
    const userLang = navigator.language;
    const headers = new HttpHeaders({
      'Accept-Language': userLang,
    });
    return this.http
      .post<AuthResponseInterface>(url, data, {headers})
      .pipe(map((response) => response.user));
  }
}
