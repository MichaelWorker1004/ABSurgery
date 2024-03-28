import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Location } from '@angular/common';

@Component({
  selector: 'abs-back-navigation',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './back-navigation.component.html',
  styleUrls: ['./back-navigation.component.scss'],
})
export class BackNavigationComponent {
  /**
   * boolean flag to hide or display the Back Button
   */
  @Input() IsCommingFromQEApp = false;

  constructor(private location: Location) {}

  goBack() {
    this.location.back();
  }
}
