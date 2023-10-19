import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { Select, Store } from '@ngxs/store';
import { ActionCardComponent } from '../shared/components/action-card/action-card.component';
import { HighlightCardComponent } from '../shared/components/highlight-card/highlight-card.component';
import { UserInformationSliderComponent } from '../shared/components/user-information-slider/user-information-slider.component';
import { ACTION_CARDS } from './user-action-cards';

import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { ButtonModule } from 'primeng/button';
import { Observable, take } from 'rxjs';
import { IExamTitleReadOnlyModel } from '../api/models/examinations/exam-title-read-only.model';
import { IAgendaReadOnlyModel } from '../api/models/examiners/agenda-read-only.model';
import { IConflictReadOnlyModel } from '../api/models/examiners/conflict-read-only.model';
import { IDashboardRosterReadOnlyModel } from '../api/models/scoring/dashboard-roster-read-only.model';
import { GlobalDialogService } from '../shared/services/global-dialog.service';
import {
  DownloadDocument,
  ExamScoringSelectors,
  GetExamHeaderId,
  GetExamTitle,
  GetExaminerAgenda,
  GetExaminerConflict,
  GetRoster,
  ResetCaseCommentsData,
  ResetExamScoringData,
  UserProfileSelectors,
} from '../state';
import { ApplicationSelectors } from '../state/application/application.selectors';
import { IFeatureFlags } from '../state/application/application.state';

@UntilDestroy()
@Component({
  selector: 'abs-ce-scoring',
  standalone: true,
  imports: [
    CommonModule,
    TranslateModule,
    ActionCardComponent,
    HighlightCardComponent,
    UserInformationSliderComponent,
    ButtonModule,
  ],
  templateUrl: './ce-scoring.component.html',
  styleUrls: ['./ce-scoring.component.scss'],
})
export class CeScoringAppComponent implements OnInit {
  @Select(ApplicationSelectors.slices.featureFlags) featureFlags$:
    | Observable<IFeatureFlags>
    | undefined;
  @Select(ExamScoringSelectors.slices.dashboardRoster) dashboardRoster$:
    | Observable<IDashboardRosterReadOnlyModel[]>
    | undefined;

  @Select(UserProfileSelectors.userId) userId$: Observable<string> | undefined;

  @Select(ExamScoringSelectors.slices.examTitle) examTitle$:
    | Observable<IExamTitleReadOnlyModel>
    | undefined;

  @Select(ExamScoringSelectors.slices.examinerAgenda)
  examinerAgenda$: Observable<IAgendaReadOnlyModel> | undefined;

  @Select(ExamScoringSelectors.slices.examinerConflict)
  examinerConflict$: Observable<IConflictReadOnlyModel> | undefined;

  @Select(ExamScoringSelectors.slices.examHeaderId) examHeaderId$:
    | Observable<number>
    | undefined;

  examinationDate = new Date().toISOString().split('T')[0];

  currentYear = new Date().getFullYear();
  userActionCards = ACTION_CARDS;
  alertsAndNotices: any[] | undefined;
  dashboardRoster!: IDashboardRosterReadOnlyModel[];
  examinationWeek!: string;

  ceScoreTesting = false;

  constructor(
    private _store: Store,
    private globalDialogService: GlobalDialogService,
    private _translateService: TranslateService
  ) {
    this, _store.dispatch(new GetExamHeaderId());
    this.featureFlags$?.pipe(untilDestroyed(this)).subscribe((featureFlags) => {
      if (featureFlags) {
        this.ceScoreTesting = <boolean>featureFlags.ceScoreTesting;
        if (featureFlags.ceScoreTesting) {
          this._store.dispatch(
            new GetExamHeaderId(featureFlags.ceScoreTesting)
          );
        }
        if (featureFlags.ceScoreTestingDate) {
          this.examinationDate = new Date('10/16/2023')
            .toISOString()
            .split('T')[0];
        }
      }

      this.examHeaderId$
        ?.pipe(untilDestroyed(this))
        .subscribe((examHeaderId) => {
          this._store.dispatch(new GetExamTitle(examHeaderId));
          this._store.dispatch(new GetExaminerAgenda(examHeaderId));
          this._store.dispatch(new GetExaminerConflict(examHeaderId));
        });
    });
  }

  ngOnInit(): void {
    this.userId$?.pipe(untilDestroyed(this)).subscribe((userId) => {
      this._store.dispatch(new GetRoster(+userId, this.examinationDate));
    });

    this.fetchCEDashboardDate();
  }

  fetchCEDashboardDate() {
    const alertsAndNotices = [
      {
        title: '',
        titleKey: 'EXAMSCORING.DASHBOARD.AGENDA_TITLE',
        content: '',
        contentKey: 'EXAMSCORING.DASHBOARD.AGENDA_SUBTITLE',
        alert: false,
        actionText: '',
        action: {},
        image:
          'https://images.pexels.com/photos/13548722/pexels-photo-13548722.jpeg',
      },
      {
        title: '',
        titleKey: 'EXAMSCORING.DASHBOARD.CONFLICTS_TITLE',
        content: '',
        contentKey: 'EXAMSCORING.DASHBOARD.CONFLICTS_SUBTITLE',
        alert: false,
        actionText: '',
        image:
          'https://images.pexels.com/photos/6098057/pexels-photo-6098057.jpeg',
        downloadLink:
          '../../assets/files/examiner_056246_daythree_ce_agenda_dr_barnhart.pdf',
      },
    ];

    alertsAndNotices.forEach((item) => {
      if (item.titleKey) {
        this._translateService
          .get(item.titleKey)
          .pipe(take(1))
          .subscribe((res) => {
            item.title = res;
          });
      }
      if (item.contentKey) {
        this._translateService
          .get(item.contentKey)
          .pipe(take(1))
          .subscribe((res) => {
            item.content = res;
          });
      }
    });

    this.examinerAgenda$
      ?.pipe(untilDestroyed(this))
      .subscribe((examinerAgenda: IAgendaReadOnlyModel) => {
        if (examinerAgenda?.id) {
          alertsAndNotices[0].action = {
            type: 'download',
            documentId: examinerAgenda.id,
            documentName: examinerAgenda.documentName,
          };
          this._translateService
            .get('EXAMSCORING.DASHBOARD.AGENDA_BTN')
            .pipe(take(1))
            .subscribe((res) => {
              alertsAndNotices[0].actionText = res;
            });
        } else {
          alertsAndNotices[0].actionText = 'Not Available';
        }
        this.globalDialogService.closeOpenDialog();
      });

    this.examinerConflict$
      ?.pipe(untilDestroyed(this))
      .subscribe((examinerConflict: IConflictReadOnlyModel) => {
        if (examinerConflict?.id) {
          alertsAndNotices[1].action = {
            type: 'download',
            documentId: examinerConflict.id,
            documentName: examinerConflict.documentName,
          };
          this._translateService
            .get('EXAMSCORING.DASHBOARD.CONFLICTS_BTN')
            .pipe(take(1))
            .subscribe((res) => {
              alertsAndNotices[1].actionText = res;
            });
        } else {
          alertsAndNotices[1].actionText = 'Not Available';
        }
        this.globalDialogService.closeOpenDialog();
      });

    this.alertsAndNotices = alertsAndNotices;

    this.dashboardRoster$
      ?.pipe(untilDestroyed(this))
      .subscribe((dashboardRoster) => {
        const newDashboardRoster = dashboardRoster?.map((roster) => {
          const item = { ...roster };
          if (item.startTime) {
            const startParts = item.startTime.split(':');
            const startHours = parseInt(startParts[0], 10);
            const startMinutes = parseInt(startParts[1], 10);
            const newStartTime = new Date();
            newStartTime.setHours(startHours);
            newStartTime.setMinutes(startMinutes);
            item.startTime = newStartTime.toLocaleTimeString('en-US', {
              hour: 'numeric',
              minute: '2-digit',
              hour12: true,
            });
          }
          if (item.endTime) {
            const endParts = item.endTime.split(':');
            const endHours = parseInt(endParts[0], 10);
            const endMinutes = parseInt(endParts[1], 10);
            const newEndTime = new Date();
            newEndTime.setHours(endHours);
            newEndTime.setMinutes(endMinutes);
            item.endTime = newEndTime.toLocaleTimeString('en-US', {
              hour: 'numeric',
              minute: '2-digit',
              hour12: true,
            });
          }
          return item;
        });
        this.dashboardRoster = newDashboardRoster;
      });

    this.examinationWeek = new Date().toLocaleDateString();
  }

  handleCardAction($event: any) {
    this._store.dispatch(new DownloadDocument($event));
  }

  resetCaseCommentsData() {
    this._store.dispatch(new ResetCaseCommentsData());
  }

  resetExamScoringData() {
    this._store.dispatch(new ResetExamScoringData());
  }
}
