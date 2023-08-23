import { Injectable } from '@angular/core';
import { ErrorModel } from '../models/error';

import { MatSnackBar } from '@angular/material/snack-bar';
import { NotificationConfig } from '../constants/notification-constants';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  constructor(
    private readonly snackBar: MatSnackBar
  ) { }

  hadleSuccess(message: string) {
    this.notifyCore(message, "success-notification-box");
  }

  handleError(error: ErrorModel) {
    this.notifyCore(error.message, "error-notification-box");
  }

  private notifyCore(message: string, className: string) {
    this.snackBar.open(message, "Close", 
    {
      horizontalPosition: NotificationConfig.horizontalPosition,
      verticalPosition: NotificationConfig.verticalPosition,
      panelClass: className,
      duration: NotificationConfig.duration,
    });
  }
}
