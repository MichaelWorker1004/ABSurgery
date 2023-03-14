import { Injectable } from '@angular/core';

@Injectable()
export class YtgHelperService {
  constructor() {}

  // FormGroup Helper Functions

  public getDirtyFormValues(form: any) {
    let dirtyValues = {};

    Object.keys(form.controls).forEach((key) => {
      let currentControl = form.controls[key];

      if (currentControl.dirty) {
        if (currentControl.controls) {
          dirtyValues[key] = this.getDirtyFormValues(currentControl);
        } else {
          dirtyValues[key] = currentControl.value;
        }
      }
    });

    return dirtyValues;
  }
}
