import { Injectable } from '@angular/core';
import { UserStorageContants } from '../constants/storage-constants';

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  saveAccessToken(accessToken: string) {
    sessionStorage.setItem(UserStorageContants.accessToken, accessToken);
  }

  saveUserId(id: string): void {
    sessionStorage.setItem(UserStorageContants.userId, id);
  }

  getUserId(id: string): string | null {
    return sessionStorage.getItem(UserStorageContants.userId);
  }
}
