import { Injectable } from '@angular/core';
import { UserStorageContants } from '../constants/storage-constants';
import { Subject } from 'rxjs';
import { AccountModel } from '../models/account';

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  user$: Subject<AccountModel> = new Subject() 

  saveTokens(accessToken: string, refreshToken: string) {
    sessionStorage.setItem(UserStorageContants.accessToken, accessToken);
    sessionStorage.setItem(UserStorageContants.refreshToken, refreshToken);
  }

  getAccessToken(): string | null {
    return sessionStorage.getItem(UserStorageContants.accessToken);
  }

  getRefreshToken(): string | null {
    return sessionStorage.getItem(UserStorageContants.refreshToken);
  }

  saveUserId(id: string): void {
    sessionStorage.setItem(UserStorageContants.userId, id);
  }

  getUserId(): string | null {
    return sessionStorage.getItem(UserStorageContants.userId);
  }

  saveUser(user: AccountModel) {
    sessionStorage.setItem(UserStorageContants.user, JSON.stringify(user));

    this.user$.next(user);
  }

  clear() {
    sessionStorage.clear();
  }
}
