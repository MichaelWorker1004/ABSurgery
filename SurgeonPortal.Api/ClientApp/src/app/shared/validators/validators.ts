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
    const value: string = control.value;
    const regex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$/gm;
    if (value && !value.match(regex)) {
      return { validatePassword: true };
    }
    return null;
  };
}

export function validateStartAndEndDates(
  startDate: string,
  endDate: string
): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const start = control.get(startDate)?.value;
    const end = control.get(endDate)?.value;

    if (start && end) {
      if (new Date(start).getTime() > new Date(end).getTime()) {
        return { datesValid: false };
      }
    }

    return null;
  };
}

export function validateMinDuration(
  startDate: string,
  endDate: string,
  minDuration: number
): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const start = control.get(startDate)?.value;
    const end = control.get(endDate)?.value;

    if (!start || !end) return null;

    const difference_in_time =
      new Date(end).getTime() - new Date(start).getTime();
    const difference_in_days = difference_in_time / (1000 * 3600 * 24) + 1;

    if (difference_in_days < minDuration) {
      return { minDurationValid: false };
    }

    return null;
  };
}

export function validateMaxDuration(
  startDate: string,
  endDate: string,
  maxDuration: number
): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const start = control.get(startDate)?.value;
    const end = control.get(endDate)?.value;

    if (!start || !end) return null;

    const difference_in_time =
      new Date(end).getTime() - new Date(start).getTime();
    const difference_in_days = difference_in_time / (1000 * 3600 * 24) + 1;

    if (difference_in_days > maxDuration) {
      return { maxDurationValid: false };
    }

    return null;
  };
}
