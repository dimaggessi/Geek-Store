import {ErrorResponseInterface} from '@shared/types/errors.interface';
import {UserInterface} from '@shared/types/user.interface';

export interface AuthStateInterface {
  isLoggedIn: boolean;
  isSubmitting: boolean;
  isLoading: boolean;
  user: UserInterface | null | undefined;
  errors: ErrorResponseInterface | null;
}
