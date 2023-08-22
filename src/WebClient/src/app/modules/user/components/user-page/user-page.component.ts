import { HttpErrorResponse } from '@angular/common/http';
import { Component, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { OpenMode } from 'src/app/core/enums/user-page-open-modes';
import { AccountModel } from 'src/app/core/models/account';
import { AccountsService } from 'src/app/core/services/accounts.service';
import { ErrorHandlerService } from 'src/app/core/services/error-handler.service';

@Component({
  selector: 'app-user-page',
  templateUrl: './user-page.component.html',
  styleUrls: ['./user-page.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserPageComponent implements OnInit {
  openMode: OpenMode = OpenMode.Create;
  user?: AccountModel;

  constructor(
    private readonly router: Router,
    private readonly route: ActivatedRoute,
    private readonly accountsService: AccountsService,
    private readonly errorHandler: ErrorHandlerService
  ) {
    this.setupPage();
  }

  ngOnInit(): void {
    this.loadUserInfo();
  }
  
  private loadUserInfo() {
    if (this.openMode !== OpenMode.Edit) return

    const id = this.route.snapshot.paramMap.get('id')!;

    this.accountsService.getById(id)
    .subscribe({
      next: (user) => this.user = user,
      error: (err) => this.handlerError(err, id)
    })
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
