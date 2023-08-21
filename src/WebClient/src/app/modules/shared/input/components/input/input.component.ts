import { AfterContentInit, ChangeDetectionStrategy, Component, Input, Self } from '@angular/core';
import { AbstractControl, ControlValueAccessor, FormControl, NgControl } from '@angular/forms';

@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class InputComponent implements ControlValueAccessor, AfterContentInit  {
  private control: AbstractControl;

  @Input() label: string = '';
  @Input() placeholder: string = '';
  @Input() isSecret: boolean = false;
  
  hideValue: boolean = true;
  value: string = '';
  onChange?: (value: any) => void;
  onTouched?: (value: any) => void;

  constructor(@Self() private readonly ngControl: NgControl) {
    ngControl.valueAccessor = this;
    this.control = ngControl.control!;
  }
  ngAfterContentInit(): void {
    this.control = this.ngControl.control!;
  }

  writeValue(value: any): void {
    this.setValue(value)
  }

  registerOnChange(fn: (value: any) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: (value: any) => void): void {
    this.onTouched = fn;
  }

  get formControl(): FormControl<string> {
    return this.control as FormControl<string>;
  }

  private setValue(value: any) {
    this.value = value;
    this.onChange && this.onChange(value);
  }
}
