import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Select, Store } from '@ngxs/store';
import { Observable, skipWhile, take } from 'rxjs';
import { TranslateModule, TranslateService } from '@ngx-translate/core';

import { ActionCardComponent } from '../shared/components/action-card/action-card.component';
import { HighlightCardComponent } from '../shared/components/highlight-card/highlight-card.component';
import { UserInformationCardComponent } from '../shared/components/user-information-card/user-information-card.component';
import {
  IUserProfile,
  UserProfileSelectors,
  DashboardSelectors,
  GetDashboardCertificationInformation,
  GetDashboardProgramInformation,
  IDashboardState,
  GetTraineeRegistrationStatus,
  GetAlertsAndNotices,
  GetExamDirectory,
  ExamProcessSelectors,
} from '../state';
import { UserClaims } from '../side-navigation/user-status.enum';
import { IActionCardReadOnlyModel } from '../shared/components/action-card/action-card-read-only.model';
import {
  CERTIFIED_ACTION_CARDS,
  TRAINEE_ACTION_CARDS,
} from './user-action-cards';
import { IProgramReadOnlyModel } from '../api/models/trainees/program-read-only.model';
import { ICertificationReadOnlyModel } from '../api/models/surgeons/certification-read-only.model';
import { ApplicationSelectors } from '../state/application/application.selectors';
import { IFeatureFlags } from '../state/application/application.state';

@UntilDestroy()
@Component({
  selector: 'abs-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    TranslateModule,
    ActionCardComponent,
    UserInformationCardComponent,
    HighlightCardComponent,
  ],
})
export class DashboardComponent {
  @Select(UserProfileSelectors.user) user$:
    | Observable<IUserProfile>
    | undefined;

  @Select(UserProfileSelectors.userClaims) userClaims$:
    | Observable<string[]>
    | undefined;

  @Select(ApplicationSelectors.slices.featureFlags) featureFlags$:
    | Observable<IFeatureFlags>
    | undefined;

  @Select(DashboardSelectors.dashboardProgramInformation) programInformation$:
    | Observable<IDashboardState>
    | undefined;

  @Select(DashboardSelectors.dashboardRegistrationStatus) registrationStatus$:
    | Observable<IDashboardState>
    | undefined;

  @Select(DashboardSelectors.dashboardCertificateInformation)
  certificateInformation$: Observable<IDashboardState> | undefined;

  @Select(DashboardSelectors.dashboardAlertsAndNotices)
  alertsAndNotices$: Observable<IDashboardState> | undefined;

  userData: IUserProfile | undefined;
  userActionCards: IActionCardReadOnlyModel[] | undefined;
  isSurgeon: boolean | undefined;
  userInformation!: IProgramReadOnlyModel | ICertificationReadOnlyModel[];

  alertsAndNotices: any | undefined;

  upcomingExams: any[] | undefined;

  featureFlags: IFeatureFlags = {};

  certifiedCards: IActionCardReadOnlyModel[] = CERTIFIED_ACTION_CARDS;
  traineeCards: IActionCardReadOnlyModel[] = TRAINEE_ACTION_CARDS;

  constructor(
    private _store: Store,
    private _translateService: TranslateService
  ) {
    this.featureFlags$?.pipe(take(1)).subscribe((featureFlags) => {
      if (featureFlags) {
        this.featureFlags = featureFlags;

        // TODO - since we are assuming content and order for these lists we should just define them in the page rather than fetching a pre-defined list
        this.traineeCards[0].disabled = !this.featureFlags.gmeHistoryPage;
        this.traineeCards[0].actionDisplay = this.featureFlags.gmeHistoryPage
          ? 'View Your GME'
          : 'Coming Soon';

        this.traineeCards[1].disabled = !this.featureFlags.applyRegisterPage;
        this.traineeCards[1].actionDisplay = this.featureFlags.applyRegisterPage
          ? 'Apply Now'
          : 'Coming Soon';

        this.certifiedCards[0].disabled =
          !this.featureFlags.continuousCertificationPage;
        this.certifiedCards[0].actionDisplay = this.featureFlags
          .continuousCertificationPage
          ? 'See Requirements'
          : 'Coming Soon';

        this.certifiedCards[1].disabled = !this.featureFlags.applyRegisterPage;
        this.certifiedCards[1].actionDisplay = this.featureFlags
          .applyRegisterPage
          ? 'Apply Now'
          : 'Coming Soon';

        this.certifiedCards[2].disabled = !this.featureFlags.cmeRepositoryPage;
        this.certifiedCards[2].actionDisplay = this.featureFlags
          .cmeRepositoryPage
          ? 'View CME Repository'
          : 'Coming Soon';
      }
    });
    this.initDashboardData();
  }

  initDashboardData() {
    this.userClaims$
      ?.pipe(
        skipWhile((userClaims) => !userClaims),
        untilDestroyed(this)
      )
      .subscribe((userClaims) => {
        const isSurgeon = userClaims?.includes(UserClaims.surgeon);
        const isTrainee = userClaims?.includes(UserClaims.trainee);
        this.isSurgeon = isSurgeon;

        if (isSurgeon) {
          this._store.dispatch(new GetDashboardCertificationInformation());
          this.certificateInformation$
            ?.pipe(untilDestroyed(this))
            .subscribe((userInformation) => {
              if (userInformation?.certificates?.length > 0) {
                this.userInformation = userInformation.certificates;
              }
            });
        } else if (isTrainee) {
          this._store.dispatch(new GetDashboardProgramInformation());
          this._store.dispatch(new GetTraineeRegistrationStatus('2022GO6'));
          this._store.dispatch(new GetAlertsAndNotices());
          this.registrationStatus$
            ?.pipe(untilDestroyed(this))
            .subscribe((userInformation) => {
              const registrationInformation =
                userInformation?.registrationStatus;
              const todaysDate = new Date();
              const regOpenDate = new Date(
                registrationInformation?.regOpenDate ?? ''
              );
              const regCloseDate = new Date(
                registrationInformation?.regEndDate ?? ''
              );
              const isRegisterDates = () => {
                if (todaysDate >= regOpenDate && todaysDate <= regCloseDate) {
                  return true;
                }
                return false;
              };

              const applyForQECard = this.traineeCards[1];
              if (
                (registrationInformation?.isRegOpen ||
                  registrationInformation?.isRegLate) &&
                isRegisterDates()
              ) {
                applyForQECard.disabled = !this.featureFlags.applyRegisterPage;
                applyForQECard.actionDisplay = this.featureFlags
                  .applyRegisterPage
                  ? this._translateService.instant(
                      'DASHBOARD.ACTION_CARDS.REGISTER_BTN'
                    )
                  : 'Coming Soon';
              } else {
                applyForQECard.disabled = true;
                applyForQECard.description = this._translateService.instant(
                  'DASHBOARD.ACTION_CARDS.APPLY_SUBTITLE',
                  {
                    date: new Date(
                      registrationInformation?.regOpenDate ?? ''
                    ).toLocaleDateString(),
                  }
                );
              }
            });
          this.programInformation$
            ?.pipe(untilDestroyed(this))
            .subscribe((userInformation) => {
              if (userInformation?.programs?.programName.length > 0) {
                this.userInformation = userInformation.programs;
              }
            });
        }

        this.setActionCardsByUserClaims(isSurgeon);
        this.fetchAlertsAndNoticesByUserId(isSurgeon);
      });
  }

  fetchAlertsAndNoticesByUserId(isSurgeon: boolean) {
    const alertsAndNoticesTrainee = [
      {
        content: this._translateService.instant(
          'DASHBOARD.HIGHLIGHT_CARDS.UPCOMINGEXAMS_SUBTITLE'
        ),
        alert: true,
        image:
          'https://images.pexels.com/photos/6098057/pexels-photo-6098057.jpeg',
      },
      {
        title: this._translateService.instant(
          'DASHBOARD.HIGHLIGHT_CARDS.DOCUMENTS_TITLE'
        ),
        content: this._translateService.instant(
          'DASHBOARD.HIGHLIGHT_CARDS.DOCUMENTS_SUBTITLE'
        ),
        action: {
          type: this.featureFlags.documentsPage ? 'component' : null,
          action: this.featureFlags.documentsPage ? '/documents' : null,
        },
        actionText: this.featureFlags.documentsPage
          ? this._translateService.instant(
              'DASHBOARD.HIGHLIGHT_CARDS.DOCUMENTS_BTN'
            )
          : 'Coming Soon',
        alert: false,
        image:
          'https://images.pexels.com/photos/4021775/pexels-photo-4021775.jpeg',
      },
    ];

    this.alertsAndNotices$
      ?.pipe(untilDestroyed(this))
      .subscribe((alertsAndNotices) => {
        let date;

        if (alertsAndNotices?.alertsAndNotices?.examStartDate) {
          date = new Date(
            alertsAndNotices?.alertsAndNotices?.examStartDate ?? ''
          ).toLocaleDateString();
          alertsAndNoticesTrainee[0].title = this._translateService.instant(
            'DASHBOARD.HIGHLIGHT_CARDS.UPCOMINGEXAMS_TITLE',
            { date: date }
          );
        }
      });

    const alertsAndNoticesCertfiied = [
      {
        title: this._translateService.instant(
          'DASHBOARD.HIGHLIGHT_CARDS.EXAMREGISTRATION_TITLE'
        ),
        content: this.upcomingExams?.join('<br>'),
        alert: true,
        image:
          'https://images.pexels.com/photos/6098057/pexels-photo-6098057.jpeg',
      },
      {
        title: this._translateService.instant(
          'DASHBOARD.HIGHLIGHT_CARDS.DOCUMENTS_TITLE'
        ),
        content: this._translateService.instant(
          'DASHBOARD.HIGHLIGHT_CARDS.DOCUMENTS_SUBTITLE'
        ),
        action: {
          type: this.featureFlags.documentsPage ? 'component' : null,
          action: this.featureFlags.documentsPage ? '/documents' : null,
        },
        actionText: this.featureFlags.documentsPage
          ? this._translateService.instant(
              'DASHBOARD.HIGHLIGHT_CARDS.DOCUMENTS_BTN'
            )
          : 'Coming Soon',
        alert: false,
        image:
          'https://images.pexels.com/photos/13548722/pexels-photo-13548722.jpeg',
      },
    ];

    if (isSurgeon) {
      this._store
        .dispatch(new GetExamDirectory())
        .pipe(untilDestroyed(this))
        .subscribe(() => {
          this.upcomingExams = this._store
            .selectSnapshot(ExamProcessSelectors.upcomingExams)
            ?.map((exam) => {
              const regOpenDate = new Date(exam.regOpenDate);
              const regCloseDate = new Date(exam.regEndDate);

              return `<b>${exam.examName}:</b> 
                <br>${regOpenDate.toLocaleDateString()} - ${regCloseDate.toLocaleDateString()}`;
            });

          alertsAndNoticesCertfiied[0].content =
            this.upcomingExams?.join('<br><br>');
        });
    }

    this.alertsAndNotices = isSurgeon
      ? alertsAndNoticesCertfiied
      : alertsAndNoticesTrainee;
  }

  setActionCardsByUserClaims(isSurgeon: boolean) {
    this.userActionCards = isSurgeon
      ? CERTIFIED_ACTION_CARDS
      : TRAINEE_ACTION_CARDS;
  }
}
