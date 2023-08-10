import { Component, OnInit, isDevMode } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActionCardComponent } from '../shared/components/action-card/action-card.component';
import { ACTION_CARDS } from './user-action-cards';
import { HighlightCardComponent } from '../shared/components/highlight-card/highlight-card.component';
import { UserInformationSliderComponent } from '../shared/components/user-information-slider/user-information-slider.component';
import { Select, Store } from '@ngxs/store';
import {
  ExamScoringSelectors,
  GetExamTitle,
  GetRoster,
  ResetCaseCommentsData,
  ResetExamScoringData,
  UserProfileSelectors,
} from '../state';
import { IRosterReadOnlyModel } from '../api/models/scoring/roster-read-only.model';
import { Observable } from 'rxjs';
import { ButtonModule } from 'primeng/button';
import { environment } from 'src/environments/environment';
import { IExamTitleReadOnlyModel } from '../api/models/examinations/exam-title-read-only.model';

@Component({
  selector: 'abs-ce-scoring',
  standalone: true,
  imports: [
    CommonModule,
    ActionCardComponent,
    HighlightCardComponent,
    UserInformationSliderComponent,
    ButtonModule,
  ],
  templateUrl: './ce-scoring.component.html',
  styleUrls: ['./ce-scoring.component.scss'],
})
export class CeScoringAppComponent implements OnInit {
  @Select(ExamScoringSelectors.slices.dashboardRoster) dashboardRoster$:
    | Observable<IRosterReadOnlyModel[]>
    | undefined;

  @Select(UserProfileSelectors.userId) userId$: Observable<string> | undefined;

  @Select(ExamScoringSelectors.slices.examTitle) examTitle$:
    | Observable<IExamTitleReadOnlyModel>
    | undefined;

  examHeaderId = 491; // TODO - remove hard coded value

  currentYear = new Date().getFullYear();
  userActionCards = ACTION_CARDS;
  alertsAndNotices: any[] | undefined;
  dashboardRoster!: IRosterReadOnlyModel[];
  examinationWeek!: string;

  examinationDate = new Date().toISOString().split('T')[0];

  isDevelopment = isDevMode();

  constructor(private _store: Store) {
    this._store.dispatch(new GetExamTitle(this.examHeaderId));
  }

  ngOnInit(): void {
    this.userId$?.subscribe((userId) => {
      this._store.dispatch(new GetRoster(+userId, this.examinationDate));
    });

    this.fetchCEDashboardDate();
  }

  fetchCEDashboardDate() {
    this.alertsAndNotices = [
      {
        title: 'Your Examination Agenda',
        content: 'Your agenda can be found here once it has been finalized.',
        alert: false,
        image:
          'https://images.pexels.com/photos/13548722/pexels-photo-13548722.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1',
        // downloadLink:
        //   'https://images.pexels.com/photos/13548722/pexels-photo-13548722.jpeg', //if the pdf is an external link
      },
    ];

    this.dashboardRoster$?.subscribe((dashboardRoster) => {
      this.dashboardRoster = dashboardRoster;
    });

    this.examinationWeek = new Date().toLocaleDateString();
  }

  resetCaseCommentsData() {
    this._store.dispatch(new ResetCaseCommentsData());
  }

  resetExamScoringData() {
    this._store.dispatch(new ResetExamScoringData());
  }
}
