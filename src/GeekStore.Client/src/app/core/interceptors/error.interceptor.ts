import {HttpErrorResponse, HttpEvent, HttpInterceptorFn} from '@angular/common/http';
import {inject} from '@angular/core';
import {Router} from '@angular/router';
import {ToastService} from '@core/services/toast.service';
import {catchError, Observable, throwError} from 'rxjs';

export const errorInterceptorFn: HttpInterceptorFn = (req, next): Observable<HttpEvent<any>> => {
  const router = inject(Router);
  const toastService = inject(ToastService);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 0) {
        toastService.show({
          message: 'O servidor não está em execução ou disponível.',
          classname: 'bg-danger text-light',
          type: 'error',
          delay: 10000,
        });
      }
      if (error.status === 400) {
        toastService.show({
          message: error.error.error.message,
          classname: 'bg-danger text-light',
          type: 'info',
          delay: 10000,
        });
      }
      if (error.status === 401) {
        toastService.show({
          message: error.error.error.message,
          classname: 'bg-danger text-light',
          type: 'error',
          delay: 10000,
        });
      }
      if (error.status === 404) {
        toastService.show({
          message: error.error.error.message,
          classname: 'bg-danger text-light',
          type: 'error',
          delay: 10000,
        });
      }
      if (error.status === 500) {
        toastService.show({
          message: error.error.error.message,
          classname: 'bg-danger text-light',
          type: 'error',
          delay: 10000,
        });
      }
      return throwError(() => error);
    })
  );
};
