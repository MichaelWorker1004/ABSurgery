import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { CollapsePanelComponent } from '../shared/components/collapse-panel/collapse-panel.component';
import { GridComponent } from '../shared/components/grid/grid.component';
import { EXAM_HISTORY_COLS } from './exam-history-cols';
import { Select, Store } from '@ngxs/store';
import { DownloadDocument, IUserProfile, UserProfileSelectors } from '../state';
import { GetExamHistory } from '../state/exam-history/exam-history.actions';
import { IExamHistoryReadOnlyModel } from '../api/models/examinations/exam-history-read-only.model';
import { ExamHistorySelectors } from '../state/exam-history';

@Component({
  selector: 'abs-examination-history',
  templateUrl: './examination-history.component.html',
  styleUrls: ['./examination-history.component.scss'],
  standalone: true,
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  imports: [CommonModule, CollapsePanelComponent, GridComponent],
})
export class ExaminationHistoryComponent {
  @Select(ExamHistorySelectors.slices.examHistory) examHistory$:
    | Observable<IExamHistoryReadOnlyModel[]>
    | undefined;

  @Select(UserProfileSelectors.user) user$:
    | Observable<IUserProfile>
    | undefined;

  tableHeightChanged$: BehaviorSubject<boolean> = new BehaviorSubject(false);

  examHistoryCols = EXAM_HISTORY_COLS;

  activeExams = [
    {
      id: 1,
      examTitle: 'Metabolic Bariatric Surgery Examination',
      startDate: new Date('3/30/22'),
      endDate: new Date('4/2/22'),
      status: 'Approved',
      updates: [
        {
          updateDate: new Date('5/2/22'),
          updateText: 'Application Packet Received',
        },
        {
          updateDate: new Date('5/2/22'),
          updateText: 'Application Received',
        },
      ],
    },
    {
      id: 2,
      examTitle: 'Metabolic Bariatric Surgery Examination',
      startDate: new Date('3/30/22'),
      endDate: new Date('4/2/22'),
      status: 'Approved',
      updates: [
        {
          updateDate: new Date('5/2/22'),
          updateText: 'Application Packet Received',
        },
        {
          updateDate: new Date('5/2/22'),
          updateText: 'Application Received',
        },
      ],
    },
  ];

  examHistory!: IExamHistoryReadOnlyModel[];

  constructor(private _router: Router, private _store: Store) {
    this._store.dispatch(new GetExamHistory());
  }

  handleGridAction($event: any) {
    const documentId = $event.data.documentId;

    if (documentId) {
      this._store.dispatch(
        new DownloadDocument({
          documentId: documentId,
          documentName: $event.data.examinationName,
        })
      );
    }
  }

  get router(): Router {
    return this._router;
  }
}
