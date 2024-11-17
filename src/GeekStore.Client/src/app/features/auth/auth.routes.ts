import {Route} from '@angular/router';
import {RegisterComponent} from './components/register/register.component';
import {LoginComponent} from './components/login/login.component';

export const registerRoute: Route[] = [
  {
    path: '',
    component: RegisterComponent,
  },
];

export const loginRoute: Route[] = [
  {
    path: '',
    component: LoginComponent,
  },
];
