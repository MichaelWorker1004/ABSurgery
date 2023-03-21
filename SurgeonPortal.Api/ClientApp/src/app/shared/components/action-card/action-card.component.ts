import {
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  Input,
  OnInit,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'abs-action-card',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './action-card.component.html',
  styleUrls: ['./action-card.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ActionCardComponent implements OnInit {
  @Input() actionCard!: any;
  actionCardClass!: string;
  highlightColor!: string;

  constructor(private _router: Router) {}

  ngOnInit(): void {
    this.setActionCardClass();
  }

  setActionCardClass() {
    const baseClass = 'action-card';

    this.actionCardClass = `${baseClass} ${this.actionCard?.status} ${
      this.actionCard?.disabled ? 'disabled' : ''
    }`;
  }

  get router(): Router {
    return this._router;
  }
}
