import {HttpInterceptorFn} from '@angular/common/http';
import {environment} from '@environments/environment';
import {delay, identity} from 'rxjs';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(environment.production ? identity : delay(500));
};
