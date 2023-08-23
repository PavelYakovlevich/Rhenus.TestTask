import { Component, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, finalize } from 'rxjs';
import { OpenMode } from 'src/app/core/constants/user-page-open-modes';
import { AccountModel } from 'src/app/core/models/account';
import { AccountsService } from 'src/app/core/services/accounts.service';
import { ErrorHandlerService } from 'src/app/core/services/error-handler.service';
import { birthdayValidator, lastNameValidator, nameValidator } from 'src/app/core/validators/validation';

@Component({
  selector: 'app-user-page',
  templateUrl: './user-page.component.html',
  styleUrls: ['./user-page.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserPageComponent implements OnInit {
  openMode: OpenMode = OpenMode.Create;
  user?: AccountModel;
  userForm: FormGroup;

  requestInProgress$: Subject<boolean> = new Subject();

  constructor(
    private readonly router: Router,
    private readonly route: ActivatedRoute,
    private readonly accountsService: AccountsService,
    private readonly errorHandler: ErrorHandlerService,
    fb: FormBuilder
  ) {
    this.setupPage();

    this.userForm = fb.group({
      name: new FormControl<string>('', nameValidator(50)),
      lastName: new FormControl<string>('', lastNameValidator(50)),
      birthday: new FormControl<string>('', birthdayValidator),
    })
  }

  ngOnInit(): void {
    this.loadUserInfo();
  }

  get cardTitle(): string {
    if (this.openMode === OpenMode.Create) return 'Create new user';

    return `User: ${this.user?.id ?? ''}`;
  }

  get actionBtnName(): string {
    if (this.openMode === OpenMode.Create) return 'Create';

    return 'Update';
  }

  get formIsValid(): boolean {
    const controls = this.userForm.controls;

    for(let control of Object.values(controls)) {
      if(!control.valid) return false;
    }

    return true;
  }

  onActionBtnWasClicked() {
    this.requestInProgress$.next(true);

    const user = this.buildUserModel();

    const observable = this.buildActionObservable(user);
    
    observable.pipe(
      finalize(() => this.requestInProgress$.next(false))
    )
    .subscribe({
      next: () => this.router.navigate(['/users']),
      error: () => this.errorHandler.handleError({
        message: "Something went wrong. Try again later"
      })
    })
  }
  
  private buildActionObservable(user: AccountModel) {
    // if (this.openMode === OpenMode.Edit) {
      return this.accountsService.update(this.user!.id, user)
    // }
  }

  private buildUserModel(): AccountModel {
    const name = this.getControlValue('name');
    const lastName = this.getControlValue('lastName');
    const birthday = this.getControlValue('birthday');

    return {
      id: this.user?.id ?? '',
      name,
      lastName,
      birthday
    }
  }
  
  private loadUserInfo() {
    if (this.openMode !== OpenMode.Edit) return

    const id = this.route.snapshot.paramMap.get('id')!;

    this.accountsService.getById(id)
    .subscribe({
      next: this.onUserWasLoaded.bind(this),
      error: (err) => this.handlerError(err, id)
    })
  }

  private onUserWasLoaded(user: AccountModel) {
    this.user = user;

    this.updateForm(user);
  }

  private updateForm(user: AccountModel) {
    this.setControlValue('name', user.name);
    this.setControlValue('lastName', user.lastName);
    this.setControlValue('birthday', user.birthday?.toLocaleString() ?? '');
  }

  private setControlValue(controlName: string, value: string) {
    const control = this.userForm.controls[controlName];

    if (!control) return;

    control.setValue(value);
  }

  private getControlValue(controlName: string) {
    const control = this.userForm.controls[controlName];
    
    return control.value;
  }

  private handlerError(_: any, id: string) {
    this.errorHandler.handleError({
      message: `Can't load user with id '${id}'`
    });

    this.router.navigate(['users']);
  }

  private setupPage() {
    const state = this.router.getCurrentNavigation()?.extras.state;
    
    if (!state) return;

    this.openMode = state["mode"];
    if (this.openMode !== OpenMode.Edit) return;
  }
}
