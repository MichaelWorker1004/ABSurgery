import { CommonModule } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA, Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Select, Store } from '@ngxs/store';
import { Observable } from 'rxjs';
import { IExamOverviewReadOnlyModel } from '../api/models/examinations/exam-overview-read-only.model';
import { IQeExamEligibilityReadOnlyModel } from '../api/models/examinations/qe-exam-eligibility-read-only.model';
import { GridComponent } from '../shared/components/grid/grid.component';
import {
  AuthSelectors,
  GetQeExamEligibility,
  IExamEligibility,
  ReqistrationRequirmentsSelectors,
} from '../state';
import { ExamProcessSelectors, GetExamDirectory } from '../state/exam-process';
import { DIRECTORY_COLS } from './directory-cols';

import { ButtonModule } from 'primeng/button';
import { ReportService } from '../api/services/reports/reports.service';

@Component({
  selector: 'abs-exam-process',
  templateUrl: './exam-process.component.html',
  styleUrls: ['./exam-process.component.scss'],
  standalone: true,
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  imports: [CommonModule, RouterLink, GridComponent, ButtonModule],
})
export class ExamProcessComponent {
  @Select(ExamProcessSelectors.slices.examDirectory) examDirectory$:
    | Observable<IExamOverviewReadOnlyModel[]>
    | undefined;

  @Select(ReqistrationRequirmentsSelectors.slices.qeExamEligibility)
  qeExamEligibility$: Observable<IExamEligibility[]> | undefined;
  directoryColumns = DIRECTORY_COLS;

  access_token = '';

  constructor(private _store: Store, private reportService: ReportService) {
    this._store.dispatch(new GetExamDirectory());
    this._store.dispatch(new GetQeExamEligibility());

    this.access_token =
      this._store.selectSnapshot(AuthSelectors.slices.access_token) || '';
  }

  downloadAdmissionCard(examCode: string, type: string) {
    this.reportService.downloadAdmissionCard(examCode, type, this.access_token);
  }
}
