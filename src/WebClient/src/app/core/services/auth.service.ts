import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthenticationConstants, authenticationHost } from '../constants/authentication-constants';
import { Observable, delay } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private readonly httpClient: HttpClient,
  ) { }

  login(email: string, password: string): Observable<any> {
    const params = this.buildHttpParams(email, password);

    return this.httpClient.post(`${authenticationHost}/connect/token`, params.toString(), {
      headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')
    }).pipe(
      delay(2000)
    );
  }

  private buildHttpParams(email: string, password: string): HttpParams {
    return new HttpParams({
      fromObject: {
        client_id: AuthenticationConstants.clientId,
        client_secret: AuthenticationConstants.clientSecret,
        grant_type: AuthenticationConstants.grantType,
        scope: AuthenticationConstants.scope,
        username: email,
        password: password
      }
    });
  }
}
