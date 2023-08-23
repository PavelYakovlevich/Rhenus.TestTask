import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { usersAPIHost } from '../constants/api-hosts';
import { Observable } from 'rxjs';
import { AccountModel } from '../models/account';
import { CreateAccountModel } from '../models/create-account';

@Injectable({
  providedIn: 'root'
})
export class AccountsService {
  constructor(
    private readonly httpClient: HttpClient
  ) { }

  getById(id: string): Observable<AccountModel> {
    return this.httpClient.get<AccountModel>(`${usersAPIHost}/accounts/${id}`);
  }

  get(skip: number, count: number): Observable<AccountModel[]> {
    return this.httpClient.get<AccountModel[]>(`${usersAPIHost}/accounts?skip=${skip}&count=${count}`);
  }

  delete(id: string) {
    return this.httpClient.delete(`${usersAPIHost}/accounts/${id}`);
  }

  update(id: string, user: AccountModel) {
    return this.httpClient.put(`${usersAPIHost}/accounts/${id}`, user);
  }

  create(user: CreateAccountModel) {
    return this.httpClient.post(`${usersAPIHost}/auth/registration`, user);
  }
}
