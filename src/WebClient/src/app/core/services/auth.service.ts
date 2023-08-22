import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthenticationConstants } from '../constants/authentication-constants';
import { Observable, delay } from 'rxjs';
import { authenticationHost } from '../constants/api-hosts';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private readonly httpClient: HttpClient,
  ) { }

  login(email: string, password: string): Observable<any> {
    const params = this.buildLoginHttpParams(email, password);

    return this.httpClient.post(`${authenticationHost}/connect/token`, params.toString(), {
      headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')
    }).pipe(
      delay(2000)
    );
  }

  refreshToken(token: string): Observable<any> {
    const params = this.buildRefreshTokenHttpParams(token);
    return this.httpClient.post(`${authenticationHost}/connect/token`, params.toString(), {
      headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')
    });
  }

  private buildLoginHttpParams(email: string, password: string): HttpParams {
    return new HttpParams({
      fromObject: {
        client_id: AuthenticationConstants.clientId,
        client_secret: AuthenticationConstants.clientSecret,
        grant_type: AuthenticationConstants.passwordGrantType,
        scope: AuthenticationConstants.scope,
        username: email,
        password: password
      }
    });
  }

  private buildRefreshTokenHttpParams(token: string): HttpParams {
    return new HttpParams({
      fromObject: {
        client_id: AuthenticationConstants.clientId,
        client_secret: AuthenticationConstants.clientSecret,
        grant_type: AuthenticationConstants.refreshTokenGrantType,
        refresh_token: token
      }
    });
  }
}
