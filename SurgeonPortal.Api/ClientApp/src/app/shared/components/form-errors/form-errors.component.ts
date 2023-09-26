import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  Input,
  OnDestroy,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { IFormErrors } from '../../common';
import { Store } from '@ngxs/store';

@Component({
  selector: 'abs-form-errors',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './form-errors.component.html',
  styleUrls: ['./form-errors.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class FormErrorsComponent implements OnDestroy {
  /**
   * An array of errors to display
   * @type {IFormErrors}
   */
  @Input() errors!: IFormErrors;

  /**
   * This should be a function that dispatches an action to clear the errors.
   * The action should come from the state.actions and be able to be used with a dispatch action from NGSX
   * @type {any}
   */
  @Input() clearErrors: any | undefined;

  /**
   * Whether or not the errors can be closed
   * @type {boolean}
   */
  @Input() closable = false;

  constructor(private store: Store) {}

  focusElement(element: string) {
    const camelCaseElement = element.charAt(0).toLowerCase() + element.slice(1);
    const el: any = document.querySelector('[id="' + camelCaseElement + '"]');
    if (el) {
      el.focus();
    }
  }

  clearFormErrors() {
    if (this.clearErrors) {
      this.store.dispatch(this.clearErrors);
    }
  }

  ngOnDestroy(): void {
    this.clearFormErrors();
  }
}
