import { inject } from "@angular/core";
import { CanActivateFn, Router } from "@angular/router";
import { LoginService } from "./Services/login.service";

export const roleGuard: CanActivateFn = (route, state) => {
  const authService = inject(LoginService); // Using `inject` to access services
  const router = inject(Router);
  const requiredRole = route.data?.['requiredRole']; // Access route data to get the required role

  if (authService.hasRole(requiredRole)) {
    return true;
  } else {
    router.navigate(['/forbidden']); // Redirect to forbidden if the role doesn't match
    return false;
  }
};