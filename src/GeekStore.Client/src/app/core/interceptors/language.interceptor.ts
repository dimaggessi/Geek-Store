import {HttpEvent, HttpInterceptorFn} from '@angular/common/http';
import {Observable} from 'rxjs';

export const languageInterceptorFn: HttpInterceptorFn = (req, next): Observable<HttpEvent<any>> => {
  const userLang = navigator.language.split(',')[0];

  const modifiedReq = req.clone({
    setHeaders: {
      'Accept-Language': userLang,
    },
  });

  return next(modifiedReq);
};
