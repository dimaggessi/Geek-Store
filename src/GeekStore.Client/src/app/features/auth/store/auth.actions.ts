import {createActionGroup, props} from '@ngrx/store';
import {RegisterRequestInterface} from '../types/registerRequest.interface';
import {UserInterface} from '@shared/types/user.interface';
import {ErrorResponseInterface} from '@shared/types/errors.interface';

export const authActions = createActionGroup({
  source: 'auth',
  events: {
    Register: props<{request: RegisterRequestInterface}>(),
    'Register success': props<{user: UserInterface}>(),
    'Register failure': props<{errors: ErrorResponseInterface}>(),
  },
});
