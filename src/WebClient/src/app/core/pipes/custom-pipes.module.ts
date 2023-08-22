import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NormalizePipe } from './pipes/normalize.pipe';



@NgModule({
  declarations: [
    NormalizePipe
  ],
  imports: [
    CommonModule
  ],
  exports: [
    NormalizePipe
  ]
})
export class CustomPipesModule { }
