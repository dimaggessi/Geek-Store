import {UserInterface} from './../../../shared/types/user.interface';
import {createActionGroup, props} from '@ngrx/store';
import {RegisterRequestInterface} from '../types/registerRequest.interface';
import {ErrorResponseInterface} from '@shared/types/errors.interface';
import {LoginRequestInterface} from '../types/loginRequest.interface';

export const authActions = createActionGroup({
  source: 'auth',
  events: {
    Register: props<{request: RegisterRequestInterface}>(),
    'Register success': props<{user: UserInterface}>(),
    'Register failure': props<{errors: ErrorResponseInterface}>(),

    Login: props<{request: LoginRequestInterface}>(),
    'Login success': props<{user: UserInterface}>(),
    'Login failure': props<{errors: ErrorResponseInterface}>(),
  },
});
