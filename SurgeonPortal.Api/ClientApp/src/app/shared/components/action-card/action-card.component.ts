import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'abs-action-card',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './action-card.component.html',
  styleUrls: ['./action-card.component.scss'],
})
export class ActionCardComponent implements OnInit {
  @Input() actionCard!: any;
  @Input() showBorderHighlight = false;
  actionCardClass!: string;
  cardStyleColor!: string;
  borderHighlightStyle!: string;
  highlightColor!: string;
  disabled!: boolean;

  constructor(private _router: Router) {}

  ngOnInit(): void {
    this.setEnabled();
    this.setHighlightColor();
    this.setActionCardClass();
    this.setCardStyleColor();
    this.setBorderHighlightStyle();
  }

  setEnabled() {
    this.disabled = this.actionCard?.action?.action ? false : true;
  }

  setHighlightColor() {
    this.highlightColor = this.actionCard?.highlightColor ?? '#DBAD6A';

    if (this.disabled) {
      this.highlightColor = '#7F7F7F';
    }
  }

  setActionCardClass() {
    this.actionCardClass = `action-card ${this.disabled ? 'disabled' : ''}`;
  }

  setCardStyleColor() {
    this.cardStyleColor = `color: ${this.highlightColor}`;
  }

  setBorderHighlightStyle() {
    this.borderHighlightStyle = `border-right: ${
      this.showBorderHighlight ? `10px solid ${this.highlightColor}` : 'none'
    }`;
  }

  get router(): Router {
    return this._router;
  }
}
