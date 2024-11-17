import {UserInterface} from '@shared/types/user.interface';

// Login, Register, GetUser, UpdateUser will have the same response
export interface AuthResponseInterface {
  user: UserInterface;
}
