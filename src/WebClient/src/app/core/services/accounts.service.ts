import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { usersAPIHost } from '../constants/api-hosts';
import { Observable } from 'rxjs';
import { AccountModel } from '../models/account';

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
}
