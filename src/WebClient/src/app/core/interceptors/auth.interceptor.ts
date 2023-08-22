import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http"
import { Injectable } from "@angular/core"
import { Observable, catchError, switchMap, throwError } from "rxjs"
import { StorageService } from "../services/storage.service"
import { AuthService } from "../services/auth.service"

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(
    private readonly storageService: StorageService,
    private readonly authService: AuthService
  ) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const accessToken = this.storageService.getAccessToken()
    if (!accessToken) {
        return next.handle(req);
    }
    
    const authReq = req.clone({
      headers: req.headers.set('Bearer', accessToken),
    })

    return next.handle(authReq).pipe(
      catchError(err => this.handleError(next, req, err))
    )
  }

  handleError(next: HttpHandler, sourceReq: HttpRequest<any>, err: HttpErrorResponse) {
    if (err.status !== 401) {
        return throwError(() => err);
    }

    const refreshToken = this.storageService.getRefreshToken()!;
    
    return this.authService.refreshToken(refreshToken).pipe(
        switchMap((result: { access_token: string, refresh_token: string }, index) => {
            this.storageService.saveTokens(result.access_token, result.refresh_token);

            const authReq = sourceReq.clone({
                headers: sourceReq.headers.set('Bearer', result.access_token),
            })

            return next.handle(authReq);
        })
    )
  }
}