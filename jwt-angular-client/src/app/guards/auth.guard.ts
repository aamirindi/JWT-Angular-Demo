import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../services/user.service';

export const authGuard: CanActivateFn = () => {
  const userService = inject(UserService);
  const router = inject(Router);

  const token = userService.getToken();
  // console.log('Guard checking token:', token);

  if (token) {
    return true;
  }

  router.navigate(['/']);
  return false;
};
