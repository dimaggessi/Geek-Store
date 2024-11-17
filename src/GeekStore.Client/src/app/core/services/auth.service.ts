import {HttpClient, HttpParams} from '@angular/common/http';
import {Inject, Injectable} from '@angular/core';
import {map, Observable} from 'rxjs';
import {UserInterface} from '@shared/types/user.interface';
import {environment} from '@environments/environment';
import {AuthResponseInterface} from '@shared/types/auth.interface';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(@Inject(HttpClient) private http: HttpClient) {}

  register(data: any): Observable<UserInterface> {
    const url = environment.apiUrl + '/auth/register';

    let params = new HttpParams();
    params = params.append('useCookies', true);

    return this.http
      .post<AuthResponseInterface>(url, data, {params, withCredentials: true})
      .pipe(map((response) => response.user));
  }

  login(data: any): Observable<void> {
    const url = environment.apiUrl + '/auth/login';

    let params = new HttpParams();
    params = params.append('useCookies', true);

    return this.http
      .post<void>(url, data, {
        params,
        withCredentials: true,
      })
      .pipe(
        map(() => {
          // TODO: Retirar do código depois do teste
          console.log('Login bem-sucedido, cookie armazenado no navegador.');
        })
      );
  }

  getUserInfo(): Observable<UserInterface> {
    const url = environment.apiUrl + '/auth/user-info';

    return this.http
      .get<AuthResponseInterface>(url, {withCredentials: true})
      .pipe(map((response) => response.user));
  }

  logout() {
    const url = environment.apiUrl + '/auth/logout';

    return this.http.post(url, {});
  }
}
