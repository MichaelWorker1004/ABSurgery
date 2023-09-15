import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Select, Store } from '@ngxs/store';
import { Observable } from 'rxjs';
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
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

@UntilDestroy()
@Component({
  selector: 'abs-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
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

  constructor(private _store: Store) {
    this.initDashboardData();
  }

  initDashboardData() {
    this.userClaims$?.pipe(untilDestroyed(this)).subscribe((userClaims) => {
      const isSurgeon = userClaims?.includes(UserClaims.surgeon);
      this.isSurgeon = isSurgeon;

      if (isSurgeon) {
        this._store.dispatch(new GetDashboardCertificationInformation());
        this.certificateInformation$?.subscribe((userInformation) => {
          if (userInformation?.certificates?.length > 0) {
            this.userInformation = userInformation.certificates;
          }
        });
      } else {
        this._store.dispatch(new GetDashboardProgramInformation());
        this._store.dispatch(new GetTraineeRegistrationStatus('2022GO6'));
        this._store.dispatch(new GetAlertsAndNotices());
        this.registrationStatus$?.subscribe((userInformation) => {
          const registrationInformation = userInformation?.registrationStatus;
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

          const applyForQECard = TRAINEE_ACTION_CARDS[1];
          if (
            (registrationInformation?.isRegOpen ||
              registrationInformation?.isRegLate) &&
            isRegisterDates()
          ) {
            applyForQECard.disabled = false;
          } else {
            applyForQECard.disabled = true;
            applyForQECard.description = `QE applications are not yet available. Check back on ${new Date(
              registrationInformation?.regOpenDate ?? ''
            ).toLocaleDateString()}`;
          }
        });
        this.programInformation$?.subscribe((userInformation) => {
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
        content:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer egestas maximus turpis id pulvinar.',
        alert: true,
        image:
          'https://images.pexels.com/photos/6098057/pexels-photo-6098057.jpeg',
      },
      {
        title: 'Documents',
        content:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer egestas maximus turpis id pulvinar.',
        action: {
          type: 'component',
          action: '/documents',
        },
        alert: false,
        image:
          'https://images.pexels.com/photos/4021775/pexels-photo-4021775.jpeg',
      },
    ];

    this.alertsAndNotices$?.subscribe((alertsAndNotices) => {
      let date;

      if (alertsAndNotices?.alertsAndNotices?.examStartDate) {
        date = new Date(
          alertsAndNotices?.alertsAndNotices?.examStartDate ?? ''
        ).toLocaleDateString();
        alertsAndNoticesTrainee[0].title = `Next General Surgery QE -  ${date}`;
      }
    });

    const alertsAndNoticesCertfiied = [
      {
        title: 'Upcoming Exam Registration',
        content: this.upcomingExams?.join('<br>'),
        alert: true,
        image:
          'https://images.pexels.com/photos/6098057/pexels-photo-6098057.jpeg',
      },
      {
        title: 'Documents',
        content:
          'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer egestas maximus turpis id pulvinar.',
        action: {
          type: 'component',
          action: '/documents',
        },
        actionText: 'View Your Documents',
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
