import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const userLang = navigator.language.split(',')[0];

    const modifiedReq = req.clone({
      setHeaders: {
        'Accept-Language': userLang,
      },
    });

    return next.handle(modifiedReq);
  }
}
