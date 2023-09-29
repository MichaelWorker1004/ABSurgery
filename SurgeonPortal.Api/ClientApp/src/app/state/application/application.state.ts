import { Injectable } from '@angular/core';
import { Action, State, StateContext, StateToken, Store } from '@ngxs/store';
import {
  CloseApplication,
  LoadApplication,
  SetExamInProgress,
  SetUnsavedChanges,
} from './application.actions';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { take, tap } from 'rxjs/operators';
import { Logout } from '../auth';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';

export interface IApplicationState {
  isLoggedIn: boolean;
  isLoaded: boolean;
  isAuth: boolean;
  isUserLoaded: boolean;
  featureFlags: IFeatureFlags | undefined;
  hasUnsavedChanges: boolean;
  examInProgress: boolean;
}

export interface IFeatureFlags {
  ceScoreTesting?: boolean;
  //page level feature flags
  dashboardPage?: boolean;
  myAccountPage?: boolean;
  personalProfilePage?: boolean;
  medicalTrainingPage?: boolean;
  professionalStandingPage?: boolean;
  cmeRepositoryPage?: boolean;
  gmeHistoryPage?: boolean;
  applyRegisterPage?: boolean;
  examHistoryPage?: boolean;
  continuousCertificationPage?: boolean;
  paymentHistoryPage?: boolean;
  documentsPage?: boolean;
  examScoringPage?: boolean;
}

export const APPLICATION_STATE_TOKEN = new StateToken<IApplicationState>(
  'application'
);

@State<IApplicationState>({
  name: APPLICATION_STATE_TOKEN,
  defaults: {
    isLoggedIn: false,
    isLoaded: false,
    isAuth: false,
    isUserLoaded: false,
    featureFlags: undefined,
    hasUnsavedChanges: false,
    examInProgress: false,
  },
})
@Injectable({ providedIn: 'root' })
export class ApplicationState {
  constructor(
    private store: Store,
    private router: Router,
    private httpClient: HttpClient,
    private globalDialogService: GlobalDialogService
  ) {}

  @Action(LoadApplication)
  loadApplication(
    ctx: StateContext<IApplicationState>
  ): Observable<IFeatureFlags> {
    const state = ctx.getState();
    return this.getFeatureFlags().pipe(
      tap((response: any) => {
        ctx.patchState({
          featureFlags: response as IFeatureFlags,
        });
      })
    );
  }

  @Action(SetUnsavedChanges)
  setUnsavedChanges(
    ctx: StateContext<IApplicationState>,
    action: SetUnsavedChanges
  ) {
    ctx.patchState({
      hasUnsavedChanges: action.hasUnsavedChanges,
    });
  }

  @Action(SetExamInProgress)
  setExamInProgress(
    ctx: StateContext<IApplicationState>,
    action: SetExamInProgress
  ) {
    ctx.patchState({
      examInProgress: action.examInProgress,
    });
  }

  @Action(CloseApplication)
  closeApplication(ctx: StateContext<IApplicationState>) {
    const hasUnsavedChanges = this.store.selectSnapshot(
      (state) => state.application.hasUnsavedChanges
    );
    if (hasUnsavedChanges) {
      this.globalDialogService
        ?.showConfirmation(
          'Unsaved Changes',
          'Do you want to navigate away. close application'
        )
        .then((result) => {
          if (result) {
            this.store
              .dispatch(new SetUnsavedChanges(false))
              .pipe(take(1))
              .subscribe(() => {
                this.store
                  .dispatch(new Logout())
                  .pipe(take(1))
                  .subscribe(() => {
                    this.store.reset({});
                  });
              });
          }
        });
    } else {
      const examInProgress = this.store.selectSnapshot(
        (state) => state.application.examInProgress
      );
      if (examInProgress) {
        this.globalDialogService
          ?.showConfirmation(
            'Exam in Progress',
            'Do you want to navigate away from the exam? <br/> Navigating away will result in an incomplete exam.'
          )
          .then((result) => {
            if (result) {
              this.store
                .dispatch(new SetExamInProgress(false))
                .pipe(take(1))
                .subscribe(() => {
                  this.store.dispatch(new Logout());
                });
            }
          });
      } else {
        this.store.dispatch(new Logout());
      }
    }
  }

  private getFeatureFlags(): Observable<IFeatureFlags> {
    return this.httpClient.get('/api/features').pipe(
      map((response: any) => {
        return response as IFeatureFlags;
      })
    );
  }

  // split up unsaved changes and exam in progress logic
  private async checkUnsavedChanges(
    title?: string,
    text?: string
  ): Promise<boolean> {
    const hasUnsavedChanges = this.store.selectSnapshot(
      (state) => state.application.hasUnsavedChanges
    );
    if (hasUnsavedChanges) {
      const result = await this.globalDialogService?.showConfirmation(
        title ?? 'Unsaved Changessdfsdfsdf',
        text ?? 'Do you want to navigate away'
      );

      if (result) {
        this.store.dispatch(new SetUnsavedChanges(false));
      }

      return result;
    }
    return true;
  }
}
