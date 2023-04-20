import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'abs-highlight-card',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './highlight-card.component.html',
  styleUrls: ['./highlight-card.component.scss'],
})
export class HighlightCardComponent implements OnInit {
  @Input() alert: any;
  imageStyleUrl!: string;
  alertClass!: string;

  constructor(private _router: Router) {}

  ngOnInit(): void {
    this.buildImageStyleUrl();
    this.setAlertClass();
  }

  buildImageStyleUrl() {
    this.imageStyleUrl = `background-image:url(${this.alert?.image ?? ''})`;
  }

  setAlertClass() {
    this.alertClass = `highlight-card ${this.alert?.alert ? 'alert' : ''}`;
  }

  get router(): Router {
    return this._router;
  }
}
