import {AuthService} from './../../../core/services/auth.service';
import {inject} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {authActions} from './auth.actions';
import {catchError, map, of, switchMap, tap} from 'rxjs';
import {UserInterface} from '@shared/types/user.interface';
import {HttpErrorResponse} from '@angular/common/http';
import {Router} from '@angular/router';

// listening for 'register' action
export const registerEffect = createEffect(
  (actions$ = inject(Actions), authService = inject(AuthService)) => {
    return actions$.pipe(
      ofType(authActions.register),
      switchMap(({request}) => {
        return authService.register(request).pipe(
          map((user: UserInterface) => {
            return authActions.registerSuccess({user});
          }),
          catchError((errorResponse: HttpErrorResponse) => {
            return of(
              authActions.registerFailure({
                errors: errorResponse.error,
              })
            );
          })
        );
      })
    );
  },
  {functional: true}
);

// redirect after register success
export const redirectAfterRegisterSuccess = createEffect(
  (actions$ = inject(Actions), router = inject(Router)) => {
    return actions$.pipe(
      ofType(authActions.registerSuccess),
      tap(() => {
        router.navigateByUrl('/');
      })
    );
  },
  {
    functional: true,
    dispatch: false,
  }
);

// listening for login action
export const loginEffect = createEffect(
  (actions$ = inject(Actions), authService = inject(AuthService)) => {
    return actions$.pipe(
      ofType(authActions.login),
      switchMap(({request}) => {
        return authService.login(request).pipe(
          switchMap(() => {
            return authService.getUserInfo().pipe(
              map((user: UserInterface) => {
                return authActions.loginSuccess({user});
              })
            );
          }),
          catchError((errorResponse: HttpErrorResponse) => {
            return of(authActions.loginFailure({errors: errorResponse.error}));
          })
        );
      })
    );
  },
  {functional: true}
);

// redirect after login success
export const redirectAfterLoginEffect = createEffect(
  (action$ = inject(Actions), router = inject(Router)) => {
    return action$.pipe(
      ofType(authActions.loginSuccess),
      tap(() => router.navigateByUrl('/'))
    );
  },
  {functional: true, dispatch: false}
);
