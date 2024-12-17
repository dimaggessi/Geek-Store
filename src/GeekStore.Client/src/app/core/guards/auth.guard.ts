import {inject} from '@angular/core';
import {AuthService} from './../../features/auth/services/auth.service';
import {CanActivateFn, Router} from '@angular/router';
import {map, take} from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  return authService.isAuthenticated().pipe(
    take(1),
    map((isAuthenticated) => {
      if (isAuthenticated) {
        return true;
      } else {
        router.navigate(['/entrar'], {queryParams: {returnUrl: state.url}});
        return false;
      }
    })
  );
};
