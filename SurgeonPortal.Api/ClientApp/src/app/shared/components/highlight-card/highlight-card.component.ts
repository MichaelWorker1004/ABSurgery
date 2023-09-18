import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';

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

  localAlert!: any;

  constructor(
    private _router: Router,
    private _translateService: TranslateService
  ) {}

  ngOnInit(): void {
    this.localAlert = this.translateAlert(this.alert);
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

  /**
   *
   * @param alert
   * @returns
   */
  private translateAlert(alert: any): any[] {
    if (alert.titleKey) {
      alert.title = this._translateService.instant(alert.titleKey);
    }
    if (alert.contentKey) {
      alert.content = this._translateService.instant(alert.contentKey);
    }
    if (alert.actionTextKey) {
      alert.actionText = this._translateService.instant(alert.actionTextKey);
    }
    return alert;
  }
}
