import {inject} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {AuthService} from '../services/auth.service';
import {authActions} from './auth.actions';
import {catchError, map, of, switchMap} from 'rxjs';
import {UserInterface} from '@shared/types/user.interface';
import {HttpErrorResponse} from '@angular/common/http';

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
