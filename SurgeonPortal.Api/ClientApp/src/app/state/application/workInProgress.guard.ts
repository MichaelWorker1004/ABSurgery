import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Router, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { Select, Store } from '@ngxs/store';
import { ApplicationSelectors } from './application.selectors';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';
import { SetExamInProgress, SetUnsavedChanges } from './application.actions';

interface CanDeactivateFn {
  canDeactivate(
    route: ActivatedRouteSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree;
}

@Injectable({
  providedIn: 'root',
})
export class WorkInProgressGuard implements CanDeactivateFn {
  @Select(ApplicationSelectors.slices.hasUnsavedChanges)
  hasUnsavedChanges$!: Observable<boolean>;

  @Select(ApplicationSelectors.slices.examInProgress)
  examInProgress$!: Observable<boolean>;

  constructor(
    private router: Router,
    private store: Store,
    private globalDialogService: GlobalDialogService
  ) {}

  /**
   *
   * @param route
   * @returns
   */
  canDeactivate():
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    const hasUnsavedChanges = this.store.selectSnapshot((state) => {
      if (state.application?.hasUnsavedChanges) {
        return state.application.hasUnsavedChanges;
      }
      return null;
    });
    if (hasUnsavedChanges) {
      return this.globalDialogService
        ?.showConfirmation('Unsaved Changes', 'Do you want to navigate away')
        .then((result) => {
          if (result) {
            this.store.dispatch(new SetUnsavedChanges(false));
          }
          return result;
        });
    } else {
      const examInProgress = this.store.selectSnapshot((state) => {
        if (state.application?.examInProgress) {
          return state.application.examInProgress;
        }
        return null;
      });
      if (examInProgress) {
        return this.globalDialogService
          ?.showConfirmation(
            'Exam in Progress',
            'Do you want to navigate away from the exam? <br/> Navigating away will result in an incomplete exam.'
          )
          .then((result) => {
            if (result) {
              this.store.dispatch(new SetExamInProgress(false));
            }
            return result;
          });
      } else {
        return true;
      }
    }
  }
}
