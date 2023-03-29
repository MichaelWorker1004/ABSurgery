import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { Router } from '@angular/router';
import { Subject, BehaviorSubject } from 'rxjs';
import { CollapsePanelComponent } from '../shared/components/collapse-panel/collapse-panel.component';
import { GridComponent } from '../shared/components/grid/grid.component';
import { EXAM_HISTORY_COLS } from './exam-history-cols';

@Component({
  selector: 'abs-examination-history',
  templateUrl: './examination-history.component.html',
  styleUrls: ['./examination-history.component.scss'],
  standalone: true,
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  imports: [CommonModule, CollapsePanelComponent, GridComponent],
})
export class ExaminationHistoryComponent {
  // this just serves as a bit flip to trigger the height reset on the collapse panel
  tableHeightChanged$: BehaviorSubject<boolean> = new BehaviorSubject(false);

  examHistoryCols = EXAM_HISTORY_COLS;
  // TODO: [Joe] dummy data, replace with real data
  user = {
    displayName: 'John Doe, M.D.',
  };
  // TODO: [Joe] dummy data, replace with real data
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
  // TODO: [Joe] dummy data, replace with real data
  examHistory = [
    {
      id: 1,
      examTitle: 'Metabolic Bariatric Surgery Examination',
      date: new Date('5/2/22'),
      status: 'Completed',
      result: 'Passed', // should this be a boolean or are there more than 2 statuses?
      expanded: true,
      updates: [
        {
          updateDate: new Date('7/19/92'),
          updateText: 'Initial Certification',
        },
      ],
    },
    {
      id: 2,
      examTitle: 'Metabolic Bariatric Surgery Examination',
      date: new Date('5/2/22'),
      status: 'Completed',
      result: 'Failed', // should this be a boolean or are there more than 2 statuses?
      expanded: true,
      updates: [
        {
          updateDate: new Date('7/19/92'),
          updateText: 'Initial Certification',
        },
        {
          updateDate: new Date('7/15/02'),
          updateText: 'Recertification',
        },
      ],
    },
    {
      id: 3,
      examTitle: 'Metabolic Bariatric Surgery Examination',
      date: new Date('5/2/22'),
      status: 'Completed',
      result: 'Passed', // should this be a boolean or are there more than 2 statuses?
      expanded: false,
      updates: [
        {
          updateDate: new Date('7/19/92'),
          updateText: 'Initial Certification',
        },
        {
          updateDate: new Date('7/15/02'),
          updateText: 'Recertification',
        },
      ],
    },
    {
      id: 4,
      examTitle: 'Metabolic Bariatric Surgery Examination',
      date: new Date('5/2/22'),
      status: 'Completed',
      result: 'Passed', // should this be a boolean or are there more than 2 statuses?
      expanded: false,
      updates: [
        {
          updateDate: new Date('7/19/92'),
          updateText: 'Initial Certification',
        },
        {
          updateDate: new Date('7/15/02'),
          updateText: 'Recertification',
        },
      ],
    },
  ];

  constructor(private _router: Router) {}

  handleGridAction($event: any) {
    if ($event.fieldKey === 'expanded') {
      this.tableHeightChanged$.next(!this.tableHeightChanged$.getValue());
    }
  }

  get router(): Router {
    return this._router;
  }
}
