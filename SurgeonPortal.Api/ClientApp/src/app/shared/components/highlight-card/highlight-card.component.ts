import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'abs-highlight-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './highlight-card.component.html',
  styleUrls: ['./highlight-card.component.scss'],
})
export class HighlightCardComponent implements OnInit {
  @Input() alert: any;
  imageStyleUrl!: string;
  alertClass!: string;

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
}
