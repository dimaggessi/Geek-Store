import {AddressInterface} from './address.interface';

export interface UserInterface {
  email: string;
  firstName: string;
  lastName: string;
  address: AddressInterface | null | undefined;
  isEmailConfirmed: boolean;
}
