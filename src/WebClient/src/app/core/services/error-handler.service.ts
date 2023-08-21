import { Injectable } from '@angular/core';
import { ErrorModel } from '../models/error';

import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService {
  constructor(
    private readonly snackBar: MatSnackBar
  ) { }

  handleError(error: ErrorModel) {
    this.snackBar.open(error.message, "Close", 
    {
      horizontalPosition: "center",
      verticalPosition: "top",
    });
  }
}
