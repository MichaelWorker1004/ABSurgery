import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
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
  @Output() action: EventEmitter<any> = new EventEmitter();
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

  handleCardAction(action: string) {
    if (action === 'download') {
      this.action.emit({
        documentId: this.alert?.action?.documentId,
        documentName: this.alert?.action.documentName,
      });
    } else {
      this.action.emit(action);
    }
  }

  get router(): Router {
    return this._router;
  }
}
