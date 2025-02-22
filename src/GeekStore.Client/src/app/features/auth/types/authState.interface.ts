import {ErrorResponseInterface} from '@shared/models/errors.interface';
import {UserInterface} from '@shared/models/user.interface';

export interface AuthStateInterface {
  isLoggedIn: boolean;
  isSubmitting: boolean;
  isLoading: boolean;
  isAdmin: boolean;
  user: UserInterface | null | undefined;
  errors: ErrorResponseInterface | null;
}
