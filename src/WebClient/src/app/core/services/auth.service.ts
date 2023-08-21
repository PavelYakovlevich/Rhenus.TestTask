import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthenticationConstants, authenticationHost } from '../constants/authentication-constants';
import { Observable, catchError, throwError } from 'rxjs';
import { ErrorHandlerService } from './error-handler.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private readonly httpClient: HttpClient,
    private readonly errorHandler: ErrorHandlerService
  ) { }

  login(email: string, password: string): Observable<any> {
    const params = this.buildHttpParams(email, password);

    return this.httpClient.post(`${authenticationHost}/connect/token`, params.toString(), {
      headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')
    }).pipe(
      catchError(this.handleError.bind(this)),
    );
  }

  private handleError(err: HttpErrorResponse, _: Observable<Object>) {
    this.errorHandler.handleError({
      status: err.status,
      message: err.error?.error_description ?? "Login failed"
    });

    return throwError(() => err);
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
