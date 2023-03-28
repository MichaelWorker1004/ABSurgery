import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function gte(val: number): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const v: number = +control.value;

    if (isNaN(v)) {
      return { gte: true, requiredValue: val };
    }

    if (v <= +val) {
      return { gte: true, requiredValue: val };
    }

    return null;
  };
}

export function matchFields(field1: string, field2: string): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const v1: string = control.get(field1)?.value;
    const v2: string = control.get(field2)?.value;

    if (v1 !== v2) {
      return { matchFields: true };
    }

    return null;
  };
}

export function validatePassword(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    // eslint-disable-next-line no-debugger
    // debugger;
    const value: string = control.value;
    const regex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$/gm;
    if (value && !value.match(regex)) {
      return { validatePassword: true };
    }
    return null;
  };
}
