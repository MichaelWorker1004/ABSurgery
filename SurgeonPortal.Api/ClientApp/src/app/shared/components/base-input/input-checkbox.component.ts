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

function MakeProvider(type: any) {
  return {
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => type),
    multi: true,
  };
}

@Component({
  selector: 'abs-input-checkbox',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  template: `
    <sl-checkbox
      #checkbox
      [name]="name"
      [value]="_value"
      [size]="size"
      [disabled]="disabled"
      [checked]="checked"
      [defaultChecked]="defaultChecked"
      [indeterminate]="indeterminate"
      [form]="form"
      [required]="required"
      (sl-change)="mapChangeEvent($event)"
      ><ng-content></ng-content
    ></sl-checkbox>
  `,
  providers: [MakeProvider(InputCheckboxComponent)],
})
export class InputCheckboxComponent implements ControlValueAccessor {
  @ViewChild('checkbox') checkbox: any;

  @Input() name: string | undefined;
  @Input() form = '';
  @Input() size: 'small' | 'medium' | 'large' = 'medium';

  _value = false;
  _disabled: boolean | string = false;
  _checked: boolean | string = false;
  _defaultChecked: boolean | string = false;
  _indeterminate: boolean | string = false;
  _required: boolean | string = false;

  @Input()
  get value(): boolean {
    return this._value;
  }
  set value(value: boolean) {
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
  get checked(): boolean | string {
    return this._checked;
  }
  set checked(value: boolean | string) {
    this._checked = value === '' || (value && value !== 'false');
  }

  @Input()
  get defaultChecked(): boolean | string {
    return this._defaultChecked;
  }
  set defaultChecked(value: boolean | string) {
    this._defaultChecked = value === '' || (value && value !== 'false');
  }

  @Input()
  get indeterminate(): boolean | string {
    return this._indeterminate;
  }
  set indeterminate(value: boolean | string) {
    this._indeterminate = value === '' || (value && value !== 'false');
  }
  @Input()
  get required(): boolean | string {
    return this._required;
  }
  set required(value: boolean | string) {
    this._required = value === '' || (value && value !== 'false');
  }

  writeValue(value: any) {
    this._value = this.checked = value;
    // warning: comment below if only want to emit on user intervention
    this.onChange(value);
  }

  onChange = (e: any) => {
    // console.log('checkbox onChange', e);
  };
  onTouched = () => {
    // console.log('checkbox onTouched');
  };
  registerOnChange(fn: (e: any) => void): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  // This is what makes the Shoelace input work with Angular forms
  mapChangeEvent($event: any) {
    this.value = this.checked = !this.value;
  }
}

/*
SHOELACE CHECKBOX API
Attributes & Properties
Name	Description	Reflects	Type	Default
name	The name of the checkbox, submitted as a name/value pair with form data.		string	''
value	The current value of the checkbox, submitted as a name/value pair with form data.		string	-
size	The checkbox's size.		'small' | 'medium' | 'large'	'medium'
disabled	Disables the checkbox.		boolean	false
checked	Draws the checkbox in a checked state.		boolean	false
indeterminate	Draws the checkbox in an indeterminate state. This is usually applied to checkboxes that represents a "select all/none" behavior when associated checkboxes have a mix of checked and unchecked states.		boolean	false
defaultChecked
(property only)	The default value of the form control. Primarily used for resetting the form control.		boolean	false
form	By default, form controls are associated with the nearest containing <form> element. This attribute allows you to place the form control outside of a form and associate it with the form that has this id. The form must be in the same document or shadow root for this to work.		string	''
required	Makes the checkbox a required field.		boolean	false
validity
(property only)	Gets the validity state object		-	-
validationMessage
(property only)	Gets the validation message		-	-
updateComplete	A read-only promise that resolves when the component has finished updating.
Learn more about attributes and properties.

Events
Name	Description	Event Detail
sl-blur	Emitted when the checkbox loses focus.	-
sl-change	Emitted when the checked state changes.	-
sl-focus	Emitted when the checkbox gains focus.	-
sl-input	Emitted when the checkbox receives input.	-
sl-invalid	Emitted when the form control has been checked for validity and its constraints aren't satisfied.	-
Learn more about listening to events.

Methods
Name	Description	Arguments
click()	Simulates a click on the checkbox.	-
focus()	Sets focus on the checkbox.
options: FocusOptions

blur()	Removes focus from the checkbox.	-
checkValidity()	Checks for validity but does not show a validation message. Returns true when valid and false when invalid.	-
getForm()	Gets the associated form, if one exists.	-
reportValidity()	Checks for validity and shows the browser's validation message if the control is invalid.	-
setCustomValidity()	Sets a custom validation message. The value provided will be shown to the user when the form is submitted. To clear the custom validation message, call this method with an empty string.
message: string
 */
