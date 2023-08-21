import { Component, ChangeDetectionStrategy } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/core/services/auth.service';
import { emailValidator, passwordValidator } from 'src/app/core/validators/validation';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginFormComponent {
  loginForm: FormGroup;

  constructor(
    private readonly fb: FormBuilder,
    private readonly authService: AuthService
    ) {
    this.loginForm = this.fb.group({
      email: new FormControl<string>('', [ emailValidator ]),
      password: new FormControl<string>('', [ passwordValidator ])
    })
  }

  onLoginBtnWasClicked(): void {
    const email = this.loginForm.controls["email"].value;
    const password = this.loginForm.controls["password"].value;

    this.authService.login(email, password)
      .subscribe(result => {
      })
  }

  formIsValid(): boolean {
    return this.controlIsInValidState("email") && this.controlIsInValidState("password")
  }

  controlIsInValidState(name: string): boolean {
    const control = this.loginForm.controls[name];

    return control.valid && control.touched;
  }
}
