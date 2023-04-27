import {
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  forwardRef,
  Input,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  ControlValueAccessor,
  FormsModule,
  NG_VALUE_ACCESSOR,
  ReactiveFormsModule,
} from '@angular/forms';

function MakeProvider(type: any) {
  return {
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => type),
    multi: true,
  };
}

@Component({
  selector: 'abs-input-select',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  template: `
    <sl-select
      [name]="name"
      [placeholder]="placeholder"
      [label]="label"
      [value]="value"
      [size]="size"
      [multiple]="multiple"
      [open]="open"
      [hoist]="hoist"
      [form]="form"
      [clearable]="clearable"
      [disabled]="disabled"
      [helpText]="helpText"
      [required]="required"
      (sl-change)="mapChangeEvent($event)"
    >
      <ng-content></ng-content>
    </sl-select>
  `,
  providers: [MakeProvider(InputSelectComponent)],
})
export class InputSelectComponent implements ControlValueAccessor {
  @Input() name: string | undefined;
  @Input() label = '';
  @Input() placeholder: string | undefined;
  @Input() displaytext: string | undefined;
  @Input() helpText = '';
  @Input() form = '';
  @Input() placement: 'top' | 'bottom' = 'bottom';
  @Input() size: 'small' | 'medium' | 'large' = 'medium';

  _value: any = '';
  _disabled: boolean | string = false;
  _open: boolean | string = false;
  _hoist: boolean | string = false;
  _filled: boolean | string = false;
  _pill: boolean | string = false;
  _multiple: boolean | string = false;
  _clearable: boolean | string = false;
  _required: boolean | string = false;
  @Input()
  get value(): string | undefined {
    return this._value;
  }
  set value(value: string | undefined) {
    if (value !== this._value) {
      this._value = value;
      this.onChange(value);
    }
  }
  @Input()
  get disabled(): boolean | string {
    return this._disabled;
  }
  set disabled(value: boolean | string) {
    this._disabled = value === '' || (value && value !== 'false');
  }
  @Input()
  get multiple(): boolean | string {
    return this._multiple;
  }
  set multiple(value: boolean | string) {
    this._multiple = value === '' || (value && value !== 'false');
  }

  // TODO: <Alan> this currently does set the component to the open state,
  //  however the selects in the ng-content are not opened. Explore
  @Input()
  get open(): boolean | string {
    return this._open;
  }
  set open(value: boolean | string) {
    this._open = value === '' || (value && value !== 'false');
  }

  @Input()
  get hoist(): boolean | string {
    return this._hoist;
  }
  set hoist(value: boolean | string) {
    this._hoist = value === '' || (value && value !== 'false');
  }

  @Input()
  get clearable(): boolean | string {
    return this._clearable;
  }
  set clearable(value: boolean | string) {
    this._clearable = value === '' || (value && value !== 'false');
  }
  @Input()
  get required(): boolean | string {
    return this._required;
  }
  set required(value: boolean | string) {
    this._required = value === '' || (value && value !== 'false');
  }

  @Input()
  get filled(): boolean | string {
    return this._filled;
  }
  set filled(value: boolean | string) {
    this._filled = value === '' || (value && value !== 'false');
  }
  @Input()
  get pill(): boolean | string {
    return this._pill;
  }
  set pill(value: boolean | string) {
    this._pill = value === '' || (value && value !== 'false');
  }

  writeValue(value: any) {
    this._value = value;
    // warning: comment below if only want to emit on user intervention
    this.onChange(value);
  }

  onChange = (_: any) => {
    // console.log('onChange');
  };
  onTouched = () => {
    // console.log('onTouched');
  };
  registerOnChange(fn: (_: any) => void): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  // This is what makes the Shoelace input work with Angular forms
  mapChangeEvent($event: any) {
    const target = $event.target as HTMLSelectElement;
    this.value = target.value;
  }
}
