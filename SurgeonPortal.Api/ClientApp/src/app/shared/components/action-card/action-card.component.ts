import {
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  EventEmitter,
  Input,
  Output,
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
export class ActionCardComponent {
  @Input() actionCard!: any;
  @Input() status!: string;
  @Input() recievedOn!: Date;
  @Output() cardAction: EventEmitter<any> = new EventEmitter();

  constructor(private _router: Router) {}

  handleCardAction(action: string) {
    this.cardAction.emit(action);
  }

  get router(): Router {
    return this._router;
  }
}
