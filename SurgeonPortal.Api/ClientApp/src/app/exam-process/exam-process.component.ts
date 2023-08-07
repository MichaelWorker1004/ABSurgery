import { CUSTOM_ELEMENTS_SCHEMA, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { GridComponent } from '../shared/components/grid/grid.component';
import { DIRECTORY_COLS } from './directory-cols';
import { ExamProcessSelectors, GetExamDirectory } from '../state/exam-process';
import { IExamOverviewReadOnlyModel } from '../api/models/examinations/exam-overview-read-only.model';
import { Observable } from 'rxjs';
import { Select, Store } from '@ngxs/store';

@Component({
  selector: 'abs-exam-process',
  templateUrl: './exam-process.component.html',
  styleUrls: ['./exam-process.component.scss'],
  standalone: true,
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  imports: [CommonModule, RouterLink, GridComponent],
})
export class ExamProcessComponent implements OnInit {
  @Select(ExamProcessSelectors.slices.examDirectory) examDirectory$:
    | Observable<IExamOverviewReadOnlyModel[]>
    | undefined;

  availableApplications: any[] = [];

  directoryColumns = DIRECTORY_COLS;
  directoryData!: any[];

  constructor(private _store: Store) {
    this._store.dispatch(new GetExamDirectory());
  }

  ngOnInit(): void {
    this.getApplications();
  }

  getApplications() {
    this.availableApplications = [
      {
        name: 'Pediatric Surgery Qualifying Exam',
        progress: 'not started',
        continuousCertNeeded: true,
        status: 'not-started',
        deadline: new Date('5/10/2022'),
      },
      {
        name: 'General Surgery Qualifying Exam',
        progress: '0/10 completed',
        continuousCertNeeded: false,
        status: 'in-progress',
        deadline: new Date('5/10/2022'),
      },
    ];
  }
}
