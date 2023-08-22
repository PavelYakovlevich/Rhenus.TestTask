import { Injectable } from '@angular/core';
import { UserStorageContants } from '../constants/storage-constants';
import { Subject } from 'rxjs';
import { UserModel } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  userLoggedIn$: Subject<boolean> = new Subject() 

  saveAccessToken(accessToken: string) {
    sessionStorage.setItem(UserStorageContants.accessToken, accessToken);
  }

  saveUserId(id: string): void {
    sessionStorage.setItem(UserStorageContants.userId, id);
  }

  getUserId(): string | null {
    return sessionStorage.getItem(UserStorageContants.userId);
  }

  saveUser(user: UserModel) {
    sessionStorage.setItem(UserStorageContants.user, JSON.stringify(user));

    this.userLoggedIn$.next(true);
  }
}
