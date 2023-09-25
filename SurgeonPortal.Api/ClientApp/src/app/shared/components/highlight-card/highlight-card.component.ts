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
  /**
   * Title to display in the card
   * @type {string}
   */
  @Input() title!: string;

  /**
   * Content to display in the card
   * @type {string}
   */
  @Input() content!: string;

  /**
   * Image to display in the card
   * @type {string}
   */
  @Input() image!: string;

  /**
   * Whether or not the card is an alert
   * @type {boolean}
   */
  @Input() alert = false;

  /**
   * Action text to display in the card
   * @type {string}
   */
  @Input() actionText!: string;

  /**
   * Type of action to perform on click
   * @type {string}
   */
  @Input() actionType!: 'action' | 'download' | 'component';

  /**
   * Action to perform on click
   * @type {string}
   */
  @Input() actionAction!: string;

  /**
   * Document id to download
   * @type {string}
   */
  @Input() documentId: string | undefined;

  /**
   * Document name to download
   * @type {string}
   */
  @Input() documentName: string | undefined;

  /**
   * Action to emit when clicked
   * @type {EventEmitter<any>}
   */
  @Output() action: EventEmitter<any> = new EventEmitter();

  imageStyleUrl!: string;
  alertClass!: string;

  constructor(private _router: Router) {}

  ngOnInit(): void {
    this.buildImageStyleUrl();
    this.setAlertClass();
  }

  buildImageStyleUrl() {
    this.imageStyleUrl = `background-image:url(${this.image ?? ''})`;
  }

  setAlertClass() {
    this.alertClass = `highlight-card ${this.alert ? 'alert' : ''}`;
  }

  handleCardAction(action: string) {
    if (action === 'download') {
      this.action.emit({
        documentId: this.documentId,
        documentName: this.documentName,
      });
    } else {
      this.action.emit(action);
    }
  }

  get router(): Router {
    return this._router;
  }
}
