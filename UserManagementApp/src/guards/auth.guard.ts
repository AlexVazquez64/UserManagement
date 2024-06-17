// auth.guard.ts
import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const isLoggedIn = authService.isLoggedIn();
  console.log("isLoggedIn:", isLoggedIn); // <-- Agregar log para verificar el valor

  if (isLoggedIn) {
    return true; // Permitir acceso a la ruta
  } else {
    router.navigate(['/login']); // Redirigir al login si no está autenticado
    return false;
  }
};

