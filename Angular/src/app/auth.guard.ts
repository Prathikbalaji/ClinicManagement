
import { CanActivateFn, Router } from '@angular/router';
import { LoginService } from '../app/Services/login.service';
import { inject } from '@angular/core';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(LoginService); 
  const router = inject(Router);

  if (authService.isAuthenticated()) {
    return true;
  } else {
    router.navigate(['/login']); 
    return false;
  }
};