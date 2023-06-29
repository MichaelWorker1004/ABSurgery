import { CUSTOM_ELEMENTS_SCHEMA, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GridComponent } from '../shared/components/grid/grid.component';
import { EXAMINATION_SCORES_COLS } from './examination-scores-cols';
import { IGridOptions } from '../shared/components/grid/grid-options.model';
import { AbsFilterType } from '../shared/components/grid/abs-grid.enum';
import { DropdownModule } from 'primeng/dropdown';
import { BehaviorSubject } from 'rxjs';
import {
  FormGroup,
  FormControl,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { ModalComponent } from '../shared/components/modal/modal.component';
import { ExaminationScoreModalComponent } from './examination-score-modal/examination-score-modal.component';

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
  currentYear = new Date().getFullYear();

  examinationScoresCols = EXAMINATION_SCORES_COLS;

  dayOptions = [] as any[];
  statusOptions = [] as any[];
  selectedDayOption!: string;
  selectedStatusOption!: string;

  gridOptions: IGridOptions = {
    showFilter: true,
    filterOn: 'candidateName',
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
  candidateData!: any;

  ngOnInit(): void {
    this.getExaminationScoresDate();
    this.setFilterOptions();
    this.handleFilter();
  }

  getExaminationScoresDate() {
    this.examinationScoresData = [
      {
        candidateId: '1',
        day: 'Day 1',
        session: 'Early Morning',
        roster: 'D',
        candidateName: 'Karla Africa',
        score: 'Pass',
        criticalFail: 'N',
        status: 'Incomplete',
        cases: [
          {
            caseId: '1',
          },
          {
            caseId: '2',
          },
          {
            caseId: '3',
          },
          {
            caseId: '4',
          },
        ],
      },
      {
        candidateId: '1',
        day: 'Day 1',
        session: 'Late Afternoon',
        roster: 'D',
        candidateName: 'Nkiruka Iseigas',
        score: 'Fail',
        criticalFail: 'Y',
        status: 'Incomplete',
      },
      {
        candidateId: '1',
        day: 'Day 1',
        session: 'Late Morning',
        roster: 'D',
        candidateName: 'Daniel Fuentes',
        score: 'Pass',
        criticalFail: 'N',
        status: 'Complete',
      },
      {
        candidateId: '1',
        day: 'Day 2',
        session: 'Late Morning',
        roster: 'D',
        candidateName: 'John Ayala',
        score: 'Fail',
        criticalFail: 'N',
        status: 'Complete',
      },
    ];

    this.filteredExaminationScoresData$.next(this.examinationScoresData);
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
    this.candidateData = event.data;
    this.showViewModal = true;
  }

  closeDialog() {
    this.showViewModal = false;
  }
}
