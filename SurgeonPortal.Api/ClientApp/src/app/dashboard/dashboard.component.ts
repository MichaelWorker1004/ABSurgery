import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
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
} from '../state';
import { UserClaims } from '../side-navigation/user-status.enum';
import { IActionCardReadOnlyModel } from '../shared/components/action-card/action-card-read-only.model';
import {
  CERTIFIED_ACTION_CARDS,
  TRAINEE_ACTION_CARDS,
} from './user-action-cards';
import { IProgramReadOnlyModel } from '../api/models/trainees/program-read-only.model';
import { ICertificationReadOnlyModel } from '../api/models/surgeons/certification-read-only.model';
import { GetStateList } from '../state/picklists';

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
export class DashboardComponent implements OnInit {
  @Select(UserProfileSelectors.user) user$:
    | Observable<IUserProfile>
    | undefined;

  @Select(UserProfileSelectors.userClaims) userClaims$:
    | Observable<string[]>
    | undefined;

  @Select(DashboardSelectors.dashboardProgramInformation) programInformation$:
    | Observable<IDashboardState>
    | undefined;

  @Select(DashboardSelectors.dashboardCertificateInformation)
  certificateInformation$: Observable<IDashboardState> | undefined;
  userData: IUserProfile | undefined;
  userActionCards: IActionCardReadOnlyModel[] | undefined;
  isSurgeon: boolean | undefined;
  userInformation!: IProgramReadOnlyModel | ICertificationReadOnlyModel[];

  alertsAndNotices: any | undefined;

  constructor(private _store: Store) {
    this.initDashboardData();
  }

  ngOnInit(): void {
    this.fetchUserData();
    this.setActionCardsByUserClaims();
    this.fetchAlertsAndNoticesByUserId();
  }

  initDashboardData() {
    this.userClaims$?.subscribe((userClaims) => {
      this.isSurgeon = userClaims?.includes(UserClaims.surgeon);
    });

    this.user$?.subscribe((user) => {
      if (this.isSurgeon) {
        if (user?.absId) {
          this._store.dispatch(
            new GetDashboardCertificationInformation(user.absId.toString())
          );
        }
      } else {
        if (user?.userId) {
          this._store.dispatch(new GetDashboardProgramInformation(user.userId));
        }
      }
    });
  }

  fetchUserData() {
    if (this.isSurgeon) {
      this.certificateInformation$?.subscribe((userInformation) => {
        if (userInformation?.certificates?.length > 0) {
          this.userInformation = userInformation.certificates;
        }
      });
    } else {
      this.programInformation$?.subscribe((userInformation) => {
        if (userInformation?.programs?.programName.length > 0) {
          this.userInformation = userInformation.programs;
        }
      });
    }
  }

  fetchAlertsAndNoticesByUserId() {
    const alertsAndNoticesTrainee = [
      {
        title: 'Next General Surgery QE - 7/2024',
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
        action: 'documents',
        alert: false,
        image:
          'https://images.pexels.com/photos/4021775/pexels-photo-4021775.jpeg',
      },
    ];

    const alertsAndNoticesCertfiied = [
      {
        title: 'Issues with your GME',
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
        action: '/documents',
        alert: false,
        image:
          'https://images.pexels.com/photos/4021775/pexels-photo-4021775.jpeg',
      },
    ];

    this.alertsAndNotices = this.isSurgeon
      ? alertsAndNoticesCertfiied
      : alertsAndNoticesTrainee;
  }

  setActionCardsByUserClaims() {
    this.userActionCards = this.isSurgeon
      ? CERTIFIED_ACTION_CARDS
      : TRAINEE_ACTION_CARDS;
  }
}
