import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject, finalize } from 'rxjs';
import { AccountModel } from 'src/app/core/models/account';
import { AccountsService } from 'src/app/core/services/accounts.service';
import { ErrorHandlerService } from 'src/app/core/services/error-handler.service';
import { StorageService } from 'src/app/core/services/storage.service';

@Component({
  selector: 'app-users-dashboard',
  templateUrl: './users-dashboard.component.html',
  styleUrls: ['./users-dashboard.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UsersDashboardComponent implements OnInit {
  getAccountInfoRequestInProgress$: Subject<boolean> = new Subject();

  constructor(
    private readonly accountsService: AccountsService,
    private readonly storageService: StorageService,
    private readonly errorHandler: ErrorHandlerService,
    private readonly router: Router
  ) {
  }

  ngOnInit(): void {
    this.loadAccountInfo();
  }

  loadAccountInfo() {
    const accountId = this.storageService.getUserId()!;

    this.accountsService.getById(accountId)
    .pipe(
      finalize(() => this.getAccountInfoRequestInProgress$.next(false))
    )
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