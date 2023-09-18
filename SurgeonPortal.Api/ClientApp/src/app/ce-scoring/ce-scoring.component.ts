import { Component, OnInit, isDevMode } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActionCardComponent } from '../shared/components/action-card/action-card.component';
import { ACTION_CARDS } from './user-action-cards';
import { HighlightCardComponent } from '../shared/components/highlight-card/highlight-card.component';
import { UserInformationSliderComponent } from '../shared/components/user-information-slider/user-information-slider.component';
import { Select, Store } from '@ngxs/store';
import { TranslateModule, TranslateService } from '@ngx-translate/core';

import {
  DownloadDocument,
  ExamScoringSelectors,
  GetExamTitle,
  GetExaminerAgenda,
  GetExaminerConflict,
  GetRoster,
  ResetCaseCommentsData,
  ResetExamScoringData,
  UserProfileSelectors,
} from '../state';
import { IRosterReadOnlyModel } from '../api/models/scoring/roster-read-only.model';
import { Observable } from 'rxjs';
import { ButtonModule } from 'primeng/button';
import { IExamTitleReadOnlyModel } from '../api/models/examinations/exam-title-read-only.model';
import { ApplicationSelectors } from '../state/application/application.selectors';
import { IFeatureFlags } from '../state/application/application.state';
import { UntilDestroy } from '@ngneat/until-destroy';
import { IAgendaReadOnlyModel } from '../api/models/examiners/agenda-read-only.model';
import { IConflictReadOnlyModel } from '../api/models/examiners/conflict-read-only.model';
import { GlobalDialogService } from '../shared/services/global-dialog.service';

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
    | Observable<IRosterReadOnlyModel[]>
    | undefined;

  @Select(UserProfileSelectors.userId) userId$: Observable<string> | undefined;

  @Select(ExamScoringSelectors.slices.examTitle) examTitle$:
    | Observable<IExamTitleReadOnlyModel>
    | undefined;

  @Select(ExamScoringSelectors.slices.examinerAgenda)
  examinerAgenda$: Observable<IAgendaReadOnlyModel> | undefined;

  @Select(ExamScoringSelectors.slices.examinerConflict)
  examinerConflict$: Observable<IConflictReadOnlyModel> | undefined;

  examHeaderId = 491; // TODO - remove hard coded value
  examinationDate = new Date('01/01/2024').toISOString().split('T')[0]; // TODO - remove hard coded value

  currentYear = new Date().getFullYear();
  userActionCards = ACTION_CARDS;
  alertsAndNotices: any[] | undefined;
  dashboardRoster!: IRosterReadOnlyModel[];
  examinationWeek!: string;

  ceScoreTesting = false;

  constructor(
    private _store: Store,
    private globalDialogService: GlobalDialogService,
    private _translateService: TranslateService
  ) {
    this._store.dispatch(new GetExamTitle(this.examHeaderId));
    this._store.dispatch(new GetExaminerAgenda(this.examHeaderId));
    this._store.dispatch(new GetExaminerConflict(this.examHeaderId));
    this.featureFlags$?.pipe().subscribe((featureFlags) => {
      if (featureFlags) {
        this.ceScoreTesting = <boolean>featureFlags.ceScoreTesting;
      }
    });
  }

  ngOnInit(): void {
    this.userId$?.subscribe((userId) => {
      this._store.dispatch(new GetRoster(+userId, this.examinationDate));
    });

    this.fetchCEDashboardDate();
  }

  fetchCEDashboardDate() {
    this.globalDialogService.showLoading();
    const alertsAndNotices = [
      {
        title: this._translateService.instant('EXAMDASH_AGENDA_TITLE'),
        content: this._translateService.instant('EXAMDASH_AGENDA_SUBTITLE'),
        alert: false,
        actionText: 'Not Available',
        action: {},
        image:
          'https://images.pexels.com/photos/13548722/pexels-photo-13548722.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1',
      },
      {
        title: this._translateService.instant('EXAMDASH_CONFLICTS_TITLE'),
        content: this._translateService.instant('EXAMDASH_CONFLICTS_SUBTITLE'),
        alert: false,
        actionText: 'Not Available',
        image:
          'https://images.pexels.com/photos/6098057/pexels-photo-6098057.jpeg',
        downloadLink:
          '../../assets/files/examiner_056246_daythree_ce_agenda_dr_barnhart.pdf',
      },
    ];

    this.examinerAgenda$?.subscribe((examinerAgenda: IAgendaReadOnlyModel) => {
      if (examinerAgenda?.id) {
        alertsAndNotices[0].action = {
          type: 'download',
          documentId: examinerAgenda.id,
          documentName: examinerAgenda.documentName,
        };
        alertsAndNotices[0].actionText = this._translateService.instant(
          'EXAMDASH_AGENDA_BTN'
        );
      }
      this.globalDialogService.closeOpenDialog();
    });

    this.examinerConflict$?.subscribe(
      (examinerConflict: IConflictReadOnlyModel) => {
        if (examinerConflict?.id) {
          alertsAndNotices[1].action = {
            type: 'download',
            documentId: examinerConflict.id,
            documentName: examinerConflict.documentName,
          };
          alertsAndNotices[1].actionText = this._translateService.instant(
            'EXAMDASH_CONFLICTS_BTN'
          );
        }
        this.globalDialogService.closeOpenDialog();
      }
    );

    this.alertsAndNotices = alertsAndNotices;

    this.dashboardRoster$?.subscribe((dashboardRoster) => {
      this.dashboardRoster = dashboardRoster;
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
