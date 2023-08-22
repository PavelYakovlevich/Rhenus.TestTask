import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OpenMode } from 'src/app/core/enums/user-page-open-modes';
import { AccountModel } from 'src/app/core/models/account';
import { AccountsService } from 'src/app/core/services/accounts.service';
import { ErrorHandlerService } from 'src/app/core/services/error-handler.service';
import { StorageService } from 'src/app/core/services/storage.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class NavbarComponent implements OnInit {
  constructor(
    readonly storageService: StorageService,
    private readonly accountsService: AccountsService,
    private readonly errorHandler: ErrorHandlerService,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    this.loadAccountInfo();
  }

  onCreateUserBtnWasClicked() {
    this.router.navigate(['users/new-users'], {
      state: {
        mode: OpenMode.Create
      }
    })
  }
  

  private onAccountInfoLoaded(model: AccountModel) {
    this.storageService.saveUser(model);
  }

  private onLoadError(_: any) {
    this.errorHandler.handleError({
      message: "Something went wrong. Try to relogin"
    });

    this.router.navigate(["auth/login"]);
  }


  private loadAccountInfo() {
    const id = this.storageService.getUserId();

    if (!id) return;

    this.accountsService.getById(id)
    .subscribe(
      {
        next: this.onAccountInfoLoaded.bind(this),
        error: (_) => this.onLoadError.bind(this)
      }
    );
  }
}
