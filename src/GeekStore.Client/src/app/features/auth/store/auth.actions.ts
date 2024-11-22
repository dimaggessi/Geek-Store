import {ErrorResponseInterface} from '@shared/models/errors.interface';
import {UserInterface} from '@shared/models/user.interface';
import {createActionGroup, emptyProps, props} from '@ngrx/store';
import {RegisterRequestInterface} from '../types/registerRequest.interface';
import {LoginRequestInterface} from '../types/loginRequest.interface';

export const authActions = createActionGroup({
  source: '[Auth]',
  events: {
    Register: props<{request: RegisterRequestInterface}>(),
    'Register Success': props<{user: UserInterface}>(),
    'Register Failure': props<{errors: ErrorResponseInterface}>(),

    Login: props<{request: LoginRequestInterface}>(),
    'Login Success': props<{user: UserInterface}>(),
    'Login Failure': props<{errors: ErrorResponseInterface}>(),

    GetUser: emptyProps(),
    'GetUser Success': props<{user: UserInterface}>(),
    'GetUser Failure': props<{errors: ErrorResponseInterface}>(),

    Logout: emptyProps(),
  },
});
