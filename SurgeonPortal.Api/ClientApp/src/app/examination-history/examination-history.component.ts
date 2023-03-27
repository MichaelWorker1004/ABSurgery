import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { Router } from '@angular/router';
import { CollapsePanelComponent } from '../shared/components/collapse-panel/collapse-panel.component';

@Component({
  selector: 'abs-examination-history',
  templateUrl: './examination-history.component.html',
  styleUrls: ['./examination-history.component.scss'],
  standalone: true,
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  imports: [CommonModule, CollapsePanelComponent],
})
export class ExaminationHistoryComponent {
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

  constructor(private _router: Router) {}

  get router(): Router {
    return this._router;
  }
}
