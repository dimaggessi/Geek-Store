import {UserInterface} from '@shared/models/user.interface';

// Login, Register, GetUser, UpdateUser will have the same response
export interface AuthResponseInterface {
  user: UserInterface;
}
