import {HttpClient, HttpParams} from '@angular/common/http';
import {Inject, Injectable} from '@angular/core';
import {map, Observable} from 'rxjs';
import {UserInterface} from '@shared/models/user.interface';
import {environment} from '@environments/environment';
import {AuthResponseInterface} from '@shared/models/auth.interface';
import {RegisterRequestInterface} from '@features/auth/types/registerRequest.interface';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(@Inject(HttpClient) private http: HttpClient) {}

  register(request: RegisterRequestInterface): Observable<UserInterface> {
    const url = environment.apiUrl + '/auth/register';

    let params = new HttpParams();
    params = params.append('useCookies', true);

    return this.http
      .post<AuthResponseInterface>(url, request, {
        params,
        withCredentials: true,
      })
      .pipe(map((response) => response.user));
  }

  login(data: any): Observable<void> {
    const url = environment.apiUrl + '/auth/login';

    let params = new HttpParams();
    params = params.append('useCookies', true);

    return this.http.post<void>(url, data, {
      params,
      withCredentials: true,
    });
  }

  logout() {
    const url = environment.apiUrl + '/auth/logout';

    return this.http.post(url, {}, {withCredentials: true});
  }

  getUserInfo(): Observable<UserInterface> {
    const url = environment.apiUrl + '/auth/user-info';

    return this.http
      .get<AuthResponseInterface>(url, {withCredentials: true})
      .pipe(
        map((response) => {
          return response.user;
        })
      );
  }

  isAuthenticated(): Observable<boolean> {
    const url = environment.apiUrl + '/auth/is-authenticated';

    return this.http.get<boolean>(url, {withCredentials: true});
  }
}
