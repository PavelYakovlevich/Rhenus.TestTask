import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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

  loadAccountInfo() {
    const accountId = this.storageService.getUserId()!;

    this.accountsService.getById(accountId)
    .subscribe(
      {
        next: this.onAccountInfoLoaded.bind(this),
        error: (_) => this.onLoadError.bind(this)
      }
    );
  }

  onAccountInfoLoaded(model: AccountModel) {
    this.storageService.saveUser(model);
  }

  onLoadError(_: any) {
    this.errorHandler.handleError({
      message: "Something went wrong. Try to relogin"
    });

    this.router.navigate(["auth/login"]);
  }
}
