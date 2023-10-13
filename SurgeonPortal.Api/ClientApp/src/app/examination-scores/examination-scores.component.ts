import { CUSTOM_ELEMENTS_SCHEMA, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GridComponent } from '../shared/components/grid/grid.component';
import { EXAMINATION_SCORES_COLS } from './examination-scores-cols';
import { IGridOptions } from '../shared/components/grid/grid-options.model';
import { AbsFilterType } from '../shared/components/grid/abs-grid.enum';
import { DropdownModule } from 'primeng/dropdown';
import { BehaviorSubject, Observable, map } from 'rxjs';
import {
  FormGroup,
  FormControl,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { ModalComponent } from '../shared/components/modal/modal.component';
import { ExaminationScoreModalComponent } from './examination-score-modal/examination-score-modal.component';
import {
  ApplicationSelectors,
  ExamScoringSelectors,
  GetExamScoresList,
  GetExamTitle,
  GetSelectedExamScores,
  IFeatureFlags,
} from '../state';
import { Select, Store } from '@ngxs/store';
import { ICaseScoreReadOnlyModel, IRosterReadOnlyModel } from '../api';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { IExamTitleReadOnlyModel } from '../api/models/examinations/exam-title-read-only.model';

@UntilDestroy()
@Component({
  selector: 'abs-examination-scores',
  standalone: true,
  imports: [
    CommonModule,
    TranslateModule,
    GridComponent,
    DropdownModule,
    FormsModule,
    ReactiveFormsModule,
    ModalComponent,
    ExaminationScoreModalComponent,
  ],
  templateUrl: './examination-scores.component.html',
  styleUrls: ['./examination-scores.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ExaminationScoresComponent implements OnInit {
  examHeaderId = 482; // TODO - remove hard coded value

  // TODO: [Joe] - remove after release 1 as part of feature/1811
  @Select(ApplicationSelectors.slices.featureFlags) featureFlags$:
    | Observable<IFeatureFlags>
    | undefined;
  @Select(ExamScoringSelectors.slices.examScoresList)
  examScores$: Observable<IRosterReadOnlyModel[]> | undefined;

  @Select(ExamScoringSelectors.slices.selectedExamScores)
  selectedExamScores$: Observable<ICaseScoreReadOnlyModel[]> | undefined;

  @Select(ExamScoringSelectors.slices.examTitle) examTitle$:
    | Observable<IExamTitleReadOnlyModel>
    | undefined;

  currentYear = new Date().getFullYear();

  examinationScoresCols = EXAMINATION_SCORES_COLS;

  dayOptions = [] as any[];
  statusOptions = [] as any[];
  selectedDayOption!: string;
  selectedStatusOption$: BehaviorSubject<string> = new BehaviorSubject('');
  selectedExamScheduleId$: BehaviorSubject<number> = new BehaviorSubject(0);

  gridOptions: IGridOptions = {
    showFilter: true,
    filterOn: 'displayName',
    placeholder: 'Search Candidates',
    filterType: AbsFilterType.Text,
  };

  examinationScoresData!: any[];
  filteredExaminationScoresData$: BehaviorSubject<any> = new BehaviorSubject(
    []
  );

  filterForm = new FormGroup({
    day: new FormControl(''),
    status: new FormControl(''),
  });

  showViewModal = false;
  candidateData$: BehaviorSubject<any> = new BehaviorSubject({});

  lockedCases = false;

  // TODO: [Joe] - remove after release 1 as part of feature/1811
  currentDay = new Date();

  constructor(private _store: Store) {
    //this._store.dispatch(new GetExamTitle(this.examHeaderId));
    // TODO: [Joe] - remove after release 1 as part of feature/1811
    this.featureFlags$?.pipe(untilDestroyed(this)).subscribe((featureFlags) => {
      if (featureFlags?.ceScoreTesting) {
        this.examHeaderId = 491;
      }
      if (featureFlags?.ceScoreTestingDate) {
        this.currentDay = new Date('10/16/2023');
      }
    });
    this._store.dispatch(new GetExamTitle(this.examHeaderId));
  }

  ngOnInit(): void {
    this.getExaminationScoresDate();
    this.examSelected();
  }

  examSelected() {
    this.selectedExamScores$
      ?.pipe(untilDestroyed(this))
      .subscribe((selectedExamScores) => {
        if (selectedExamScores?.length > 0) {
          let startTime = new Date(selectedExamScores[0].examDate);
          startTime = new Date(
            startTime.toLocaleDateString() +
              ', ' +
              selectedExamScores[0].startTime
          );
          let endTime = new Date(selectedExamScores[0].examDate);
          endTime = new Date(
            endTime.toLocaleDateString() + ', ' + selectedExamScores[0].endTime
          );

          this.lockedCases = selectedExamScores.every(
            (score) => score.isLocked === true
          );

          const newCandidateData = {
            candidateName:
              selectedExamScores[0].examineeFirstName +
              ' ' +
              selectedExamScores[0].examineeLastName,
            startTime: startTime,
            endTime: endTime,
            allLocked: this.lockedCases,
            cases: selectedExamScores,
          };
          this.candidateData$.next(newCandidateData);
        }
      });
  }

  getExaminationScoresDate() {
    this._store.dispatch(new GetExamScoresList(this.examHeaderId));

    this.examScores$
      ?.pipe(
        untilDestroyed(this),
        map((scoreList) => {
          // TODO: [Joe] - remove hardcoded dates after release 1 as part of feature/1811
          const hardcodedDates = ['10/16/2023', '10/17/2023', '10/18/2023'];
          if (scoreList?.length > 0) {
            return scoreList.map((score) => {
              return {
                ...score,
                day: 'Day ' + score.dayNumber,
                // TODO: [Joe] - remove date atrribute after release 1 as part of feature/1811
                date: hardcodedDates[score.dayNumber - 1],
                session: 'Session ' + score.sessionNumber,
                status: score.isSubmitted ? 'Complete' : 'Incomplete',
                //cases: score.cases,
              };
            });
          }
          return [];
        })
      )
      .subscribe((scoreList) => {
        // TODO: [Joe] - after release 1 remove the filter and update the SP to handle the filtering
        // part of feature/1811
        // this.examinationScoresData = scoreList
        const date = this.currentDay.toLocaleDateString();

        this.examinationScoresData = scoreList.filter((exam) => {
          return exam.date === date;
        });
        this.filteredExaminationScoresData$.next(this.examinationScoresData);
        this.setFilterOptions();
        this.handleFilter();
      });
  }

  setFilterOptions() {
    this.examinationScoresData.forEach((data) => {
      if (!this.dayOptions.includes(data.day)) {
        this.dayOptions.push(data.day);
      }

      if (!this.statusOptions.includes(data.status)) {
        this.statusOptions.push(data.status);
      }
    });
  }

  handleFilter() {
    this.filterForm.valueChanges.subscribe((value) => {
      let filteredData = this.examinationScoresData;

      filteredData.filter(() => {
        if (value.day) {
          filteredData = filteredData.filter((data) => value.day === data.day);
        }

        if (value.status) {
          filteredData = filteredData.filter(
            (data) => value.status === data.status
          );
        }
      });

      this.filteredExaminationScoresData$.next(filteredData);
    });
  }

  handleView(event: any) {
    const examScheduleId = event.data.examScheduleId;
    this.selectedStatusOption$.next(event.data.status);
    this.selectedExamScheduleId$.next(examScheduleId);
    this._store.dispatch(new GetSelectedExamScores(examScheduleId));
    this.showViewModal = true;
  }

  closeDialog($event?: any) {
    if ($event) {
      this._store.dispatch(new GetExamScoresList(this.examHeaderId));
    }
    this.showViewModal = false;
  }
}
