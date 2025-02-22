import {inject} from '@angular/core';
import {CanActivateFn, Router} from '@angular/router';
import {ToastService} from '@core/services/toast.service';
import {UserInterface} from '@shared/models/user.interface';

export const adminGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const toast = inject(ToastService);
  const auth = localStorage.getItem('auth');

  if (auth) {
    const user: UserInterface = JSON.parse(auth).user;
    // console.log('user adminGuard', user);

    const isAdmin = Array.isArray(user.roles)
      ? user.roles.includes('Admin')
      : user.roles === 'Admin';

    if (isAdmin) {
      return true;
    }
  }

  toast.show({
    message: 'Acesso não autorizado.',
    type: 'error',
    classname: 'bg-danger text-white text-center',
  });
  router.navigateByUrl('/loja');
  return false;
};
