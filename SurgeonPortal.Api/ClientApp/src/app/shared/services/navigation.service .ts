import { Injectable } from '@angular/core';
import { Location } from '@angular/common';

@Injectable({
  providedIn: 'root',
})
export class NavigationService {
  private isQEcomponent = false;

  constructor(private location: Location) {}

  goBack() {
    this.location.back();
  }

  setQEcomponent(isQEcomponent: boolean) {
    this.isQEcomponent = isQEcomponent;
  }

  getQEcomponent(): boolean {
    if (this.isQEcomponent === true) {
      this.isQEcomponent = false;
      return true;
    }
    return false;
  }
}
