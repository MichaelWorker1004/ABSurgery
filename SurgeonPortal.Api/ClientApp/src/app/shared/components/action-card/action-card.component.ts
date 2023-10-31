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
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { distinctUntilChanged } from 'rxjs';

@UntilDestroy()
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

  /**
   * Title to display in the card
   * @type {string}
   */
  @Input() title!: string | undefined;

  /**
   * Title to display in the card via translation key
   * @type {string}
   */
  @Input() titleKey!: string | undefined;

  /**
   * Description to display in the card
   * @type {string}
   */
  @Input() description!: string | undefined;

  /**
   * Description to display in the card via translation key
   * @type {string}
   */
  @Input() descriptionKey!: string | undefined;

  /**
   * Whether or not the card is disabled
   * @type {boolean}
   */
  @Input() disabled: boolean | undefined = false;

  /**
   * Type of action to perform
   * @type {string}
   */
  @Input() actionType!: string | 'dialog' | 'component';

  /**
   * Action being passed to the parent component
   * @type {string}
   */
  @Input() actionAction: string | undefined = '';

  /**
   * Style of action to perform
   * @type {string}
   */
  @Input() actionStyle: 'link' | 'button' = 'link';

  // @Input() action: {
  //   type: 'component',
  //   action: '/ce-scoring/examination-rosters',
  // }

  /**
   * Action display to show in the card
   * @type {string}
   */
  @Input() actionDisplay!: string | undefined;

  /**
   * Action display to show in the card via translation key
   * @type {string}
   */
  @Input() actionDisplayKey!: string | undefined;

  /**
   * Icon to display in the card. Must be a font awesome icon
   * @type {string}
   */
  @Input() icon!: string | undefined;

  /**
   * Status of the card
   * @type {string}
   */
  @Input() status!: 'completed' | 'in-progress' | 'contingent' | 'alert';

  /**
   * Whether or not to display the status text
   * @type {boolean}
   */
  @Input() displayStatusText = true;

  /**
   * Date the card was recieved
   * @type {Date}
   */
  @Input() recievedOn!: Date;

  /**
   * Action to perform when the card is clicked
   * @type {EventEmitter<any>}
   */
  @Output() cardAction: EventEmitter<any> = new EventEmitter();

  localActionCard!: any;

  constructor(
    private _router: Router,
    private _translateService: TranslateService
  ) {}

  ngOnInit(): void {
    this.translateActionCard();
  }

  handleCardAction(action?: string) {
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
  private translateActionCard(): any {
    if (this.titleKey) {
      this._translateService
        .get(this.titleKey)
        .pipe(distinctUntilChanged(), untilDestroyed(this))
        .subscribe((res) => {
          this.title = res;
        });
    }
    if (this.descriptionKey) {
      this._translateService
        .get(this.descriptionKey)
        .pipe(distinctUntilChanged(), untilDestroyed(this))
        .subscribe((res) => {
          this.description = res;
        });
    }
    if (this.actionDisplayKey) {
      this._translateService
        .get(this.actionDisplayKey)
        .pipe(distinctUntilChanged(), untilDestroyed(this))
        .subscribe((res) => {
          this.actionDisplay = res;
        });
    }
  }
}
