import {
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  forwardRef,
  Input,
  ViewChild,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  ControlValueAccessor,
  FormsModule,
  NG_VALUE_ACCESSOR,
  ReactiveFormsModule,
} from '@angular/forms';

// This makes the component into a form control by forwarding the value accessor
function MakeProvider(type: any) {
  return {
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => type),
    multi: true,
  };
}

@Component({
  selector: 'abs-input-radio-group',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  template: `
    <sl-radio-group
      [name]="name"
      [label]="label"
      [value]="value"
      [size]="size"
      [form]="form"
      [required]="required"
      (sl-change)="mapChangeEvent($event)"
    >
      <ng-content></ng-content>
    </sl-radio-group>
  `,
  providers: [MakeProvider(InputRadioGroupComponent)], // Super important
})
export class InputRadioGroupComponent implements ControlValueAccessor {
  @ViewChild('radioGroup') radioGroup: any;

  // Implement the API for the specific form control.
  @Input() name: string | undefined;
  @Input() label = '';
  @Input() form = '';
  @Input() size: 'small' | 'medium' | 'large' = 'medium';

  _value: any = '';
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

  // For attributes that can have a shorthand, get/set functions are needed
  @Input()
  get required(): boolean | string {
    return this._required;
  }
  set required(value: boolean | string) {
    this._required = value === '' || (value && value !== 'false');
  }

  // This method is fired when the control is initially written by the form
  // control. This is where you want to write the initial value to the
  // component.
  // This mainly made a big difference for checkboxes and radio buttons
  writeValue(value: any) {
    this._value = value;
    // warning: comment below if only want to emit on user intervention
    this.onChange(value);
  }

  // Some of these are left empty but are here to satisfy the interface...
  // I think
  onChange = (e: any) => {
    // console.log('onChange');
  };
  onTouched = () => {
    // console.log('onTouched');
  };
  // Very important
  registerOnChange(fn: (e: any) => void): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  // This is what makes the input work with Angular forms
  // It is basically a listener for the sl-change event to do what is needed for the
  // form.
  mapChangeEvent($event: any) {
    const target = $event.target as HTMLSelectElement;
    this.value = target.value;
  }
}
