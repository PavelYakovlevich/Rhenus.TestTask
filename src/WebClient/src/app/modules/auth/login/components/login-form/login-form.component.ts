import { HttpErrorResponse } from '@angular/common/http';
import { Component, ChangeDetectionStrategy } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject, finalize } from 'rxjs';
import { AuthService } from 'src/app/core/services/auth.service';
import { ErrorHandlerService } from 'src/app/core/services/error-handler.service';
import { StorageService } from 'src/app/core/services/storage.service';
import { getDecodedAccessToken } from 'src/app/core/utils/jwt-utils';
import { emailValidator, passwordValidator } from 'src/app/core/validators/validation';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginFormComponent {
  loginForm: FormGroup;
  requestInProgress$: Subject<boolean> = new Subject();

  constructor(
    fb: FormBuilder,
    private readonly authService: AuthService,
    private readonly userStorageService: StorageService,
    private readonly errorHandler: ErrorHandlerService,
    private readonly router: Router
    ) {
    this.loginForm = fb.group({
      email: new FormControl<string>('', [ emailValidator ]),
      password: new FormControl<string>('', [ passwordValidator ])
    })
  }

  onLoginBtnWasClicked(): void {
    const email = this.loginForm.controls["email"].value;
    const password = this.loginForm.controls["password"].value;

    this.requestInProgress$.next(true);

    this.authService.login(email, password)
      .pipe(
        finalize(() => this.requestInProgress$.next(false))
      )
      .subscribe({
        next: this.onSuccesfullLogin.bind(this),
        error: this.handleError.bind(this)
      });
  }

  formIsValid(): boolean {
    return this.controlIsInValidState("email") && this.controlIsInValidState("password")
  }

  controlIsInValidState(name: string): boolean {
    const control = this.loginForm.controls[name];

    return control.valid && control.touched;
  }

  onSuccesfullLogin(loginResult: { access_token: string, refresh_token: string }) {
    const decodedJwt = getDecodedAccessToken(loginResult.access_token);

    this.userStorageService.saveTokens(loginResult.access_token, loginResult.refresh_token);
    this.userStorageService.saveUserId(decodedJwt.sub);

    this.router.navigate(['./users']);
  }

  handleError(err: HttpErrorResponse) {
    console.log(err)

    this.errorHandler.handleError({
      status: err.status,
      message: err.error?.error_description ?? "Login failed"
    });
  }
}
