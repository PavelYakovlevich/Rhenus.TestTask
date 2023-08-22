import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { StorageService } from '../services/storage.service';

export const userAuthenticatedGuard: CanActivateFn = (route, state) => {
  const storageService = inject(StorageService);
  const router = inject(Router);
  
  if (storageService.getUserId() === null) {
    router.navigateByUrl('/auth/login');
    return false;
  }

  return true;
};
