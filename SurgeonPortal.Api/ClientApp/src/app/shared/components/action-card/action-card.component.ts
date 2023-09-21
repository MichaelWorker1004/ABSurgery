import {
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';

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
  @Input() status!: string;
  @Input() recievedOn!: Date;
  @Output() cardAction: EventEmitter<any> = new EventEmitter();

  localActionCard!: any;

  constructor(
    private _router: Router,
    private _translateService: TranslateService
  ) {}

  ngOnInit(): void {
    this.localActionCard = this.translateActionCard(this.actionCard);
  }

  handleCardAction(action: string) {
    this.cardAction.emit(action);
  }

  get router(): Router {
    return this._router;
  }

  /**
   *
   * @param alert
   * @returns
   */
  private translateActionCard(actionCard: any): any {
    if (actionCard.titleKey) {
      actionCard.title = this._translateService.instant(actionCard.titleKey);
    }
    if (actionCard.descriptionKey) {
      actionCard.description = this._translateService.instant(
        actionCard.descriptionKey
      );
    }
    if (actionCard.actionDisplayKey) {
      actionCard.actionDisplay = this._translateService.instant(
        actionCard.actionDisplayKey
      );
    }
    return actionCard;
  }
}
