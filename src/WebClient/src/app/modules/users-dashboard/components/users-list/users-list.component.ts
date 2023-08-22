import { HttpErrorResponse } from '@angular/common/http';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { BehaviorSubject, Subject, delay, finalize } from 'rxjs';
import { AccountModel } from 'src/app/core/models/account';
import { AccountsService } from 'src/app/core/services/accounts.service';
import { ErrorHandlerService } from 'src/app/core/services/error-handler.service';
import { StorageService } from 'src/app/core/services/storage.service';
import { ConfirmationDialogComponent } from 'src/app/modules/shared/confirmation-dialog/components/confirmation-dialog/confirmation-dialog.component';

const userPerRequestCount = 10;

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.scss'],
  changeDetection: ChangeDetectionStrategy.Default
})
export class UsersListComponent implements OnInit {
  accounts: AccountModel[] = [];

  accounts$ = new BehaviorSubject<AccountModel[]>([]);
  getAccountsRequestInProgress$ = new Subject<boolean>();

  currentUserId: string = ''

  constructor(
    private readonly accountsService: AccountsService,
    private readonly errorHandler: ErrorHandlerService,
    private readonly storageService: StorageService,
    public confirmationDialog: MatDialog
  ) {
  }

  ngOnInit(): void {
    this.currentUserId = this.storageService.getUserId()!;
    this.loadAccountsChunk();
  }

  openConfirmationDialog(id: string) {
    const dialogRef = this.confirmationDialog.open(ConfirmationDialogComponent, {
      data: {
        title: 'Delete user',
        message: `Do you really want to delete user '${id}'?`
      }
    });

    dialogRef.afterClosed().subscribe(deletionConfirmed => {
      if (!deletionConfirmed) {
        return;
      }

      this.accountsService.delete(id)
      .subscribe(
        {
          next: () => this.onAccountDeleted(id),
          error: (_) => this.onRequestError.bind(this)
        }
      );
    })
  }

  onLoadMoreBtnWasClicked() {
    this.loadAccountsChunk();
  }

  private loadAccountsChunk() {
    this.getAccountsRequestInProgress$.next(true);

    this.accountsService.get(this.accounts.length, userPerRequestCount)
    .pipe(
      finalize(() => this.getAccountsRequestInProgress$.next(false))
    )
    .subscribe(
      {
        next: this.onAccountsLoaded.bind(this),
        error: (_) => this.onRequestError.bind(this)
      }
    );
  }

  private onAccountsLoaded(result: AccountModel[]) {
    this.accounts = this.accounts.concat(result);

    this.accounts$.next(this.accounts);
  }

  private onAccountDeleted(id: string) {
    this.accounts = this.accounts.filter(account => account.id !== id);

    this.accounts$.next(this.accounts);
  }

  private onRequestError(_: HttpErrorResponse) {
    this.errorHandler.handleError({
      message: "Error occured. Try again later"
    });
  }
}
