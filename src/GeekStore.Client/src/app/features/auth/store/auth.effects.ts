import {SignalrService} from '@core/services/signalr.service';
import {AuthService} from '@features/auth/services/auth.service';
import {inject} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {authActions} from './auth.actions';
import {catchError, map, of, switchMap, tap} from 'rxjs';
import {UserInterface} from '@shared/models/user.interface';
import {HttpErrorResponse} from '@angular/common/http';
import {ActivatedRoute, Router} from '@angular/router';

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
  (
    actions$ = inject(Actions),
    authService = inject(AuthService),
    signalRService = inject(SignalrService)
  ) => {
    return actions$.pipe(
      ofType(authActions.login),
      switchMap(({request}) => {
        return authService.login(request).pipe(
          switchMap(() => {
            return authService.getUserInfo().pipe(
              tap(() => signalRService.createHubConnection()),
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
  (action$ = inject(Actions), router = inject(Router), activatedRoute = inject(ActivatedRoute)) => {
    return action$.pipe(
      ofType(authActions.loginSuccess),
      tap(() => {
        const url = activatedRoute.snapshot.queryParams['returnUrl'];
        if (url) {
          router.navigateByUrl(url);
        } else {
          router.navigateByUrl('/loja');
        }
      })
    );
  },
  {functional: true, dispatch: false}
);

export const getUserEffect = createEffect(
  (
    actions$ = inject(Actions),
    authService = inject(AuthService),
    signalRService = inject(SignalrService)
  ) => {
    return actions$.pipe(
      ofType(authActions.getUser),
      switchMap(() => {
        return authService.isAuthenticated().pipe(
          switchMap((isAuth) => {
            if (!isAuth) {
              console.log('User is authenticated:', isAuth);
              return of(
                authActions.getUserFailure({
                  errors: {error: {code: 'error', message: 'cookie not found'}},
                })
              );
            }
            return authService.getUserInfo().pipe(
              tap(() => signalRService.createHubConnection()),
              map((user: UserInterface) => {
                return authActions.getUserSuccess({user});
              })
            );
          })
        );
      }),
      catchError((errorResponse: HttpErrorResponse) => {
        return of(authActions.getUserFailure({errors: errorResponse}));
      })
    );
  },
  {functional: true}
);

export const logoutEffect = createEffect(
  (
    actions$ = inject(Actions),
    router = inject(Router),
    authService = inject(AuthService),
    signalrService = inject(SignalrService)
  ) => {
    return actions$.pipe(
      ofType(authActions.logout),
      switchMap(() => {
        return authService.logout().pipe(
          tap(() => {
            localStorage.removeItem('auth');
            signalrService.stopHubConnection();
            router.navigateByUrl('/');
          })
        );
      })
    );
  },
  {functional: true, dispatch: false}
);
