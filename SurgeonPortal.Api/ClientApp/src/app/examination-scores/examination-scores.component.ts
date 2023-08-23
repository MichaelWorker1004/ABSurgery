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
import { ModalComponent } from '../shared/components/modal/modal.component';
import { ExaminationScoreModalComponent } from './examination-score-modal/examination-score-modal.component';
import {
  ExamScoringSelectors,
  GetExamScoresList,
  GetExamTitle,
  GetSelectedExamScores,
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
    GridComponent,
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
  examHeaderId = 491;

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
  selectedStatusOption!: string;

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

  constructor(private _store: Store) {
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
          if (scoreList?.length > 0) {
            return scoreList.map((score) => {
              return {
                ...score,
                day: 'Day ' + score.dayNumber,
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
        this.examinationScoresData = scoreList;
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
    this._store.dispatch(new GetSelectedExamScores(event.data.examScheduleId));
    this.showViewModal = true;
  }

  closeDialog() {
    this.showViewModal = false;
  }
}
