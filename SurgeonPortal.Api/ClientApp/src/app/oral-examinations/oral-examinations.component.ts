import { CUSTOM_ELEMENTS_SCHEMA, Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { GlobalDialogService } from '../shared/services/global-dialog.service';
import { GridComponent } from '../shared/components/grid/grid.component';
import { ORAL_EXAMINATION_COLS } from './oral-examination-cols';

import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { IExamSessionReadOnlyModel } from '../api/models/scoring/exam-session-read-only.model';

@Component({
  selector: 'abs-oral-examinations',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    GridComponent,
    InputTextModule,
    ButtonModule,
  ],
  templateUrl: './oral-examinations.component.html',
  styleUrls: ['./oral-examinations.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class OralExaminationsComponent implements OnInit {
  examDate: Date = new Date();
  zoomLink = 'https://zoom.us/j/1234567890';
  oralExaminations$: any[] = []; //eventually load from datastore with selector
  oralExaminationCols = ORAL_EXAMINATION_COLS;

  constructor(
    private _route: Router,
    private _globalDialogService: GlobalDialogService
  ) {}

  ngOnInit(): void {
    this.getOralExaminations();
  }

  getOralExaminations() {
    const examList = [
      {
        firstName: 'John',
        lastName: 'Doe',
        startTime: '9:00 AM',
        endTime: '9:30 AM',
        meetingLink: 'https://zoom.us/j/1234567890',
        isSubmitted: true,
        isCurrentSession: false,
        sessionNumber: 1,
      },
      {
        firstName: 'Jane',
        lastName: 'Doe',
        startTime: '9:30 AM',
        endTime: '10:00 AM',
        meetingLink: 'https://zoom.us/j/1234567890',
        isSubmitted: true,
        isCurrentSession: false,
        sessionNumber: 2,
      },
      {
        firstName: 'Bruce',
        lastName: 'Wayne',
        startTime: '10:00 AM',
        endTime: '10:30 AM',
        meetingLink: 'https://zoom.us/j/1234567890',
        isSubmitted: false,
        isCurrentSession: true,
        sessionNumber: 3,
      },
      {
        firstName: 'Clark',
        lastName: 'Kent',
        startTime: '10:30 AM',
        endTime: '11:00 AM',
        meetingLink: 'https://zoom.us/j/1234567890',
        isSubmitted: false,
        isCurrentSession: false,
        sessionNumber: 4,
      },
      {
        firstName: 'Tony',
        lastName: 'Stark',
        startTime: '11:00 AM',
        endTime: '11:30 AM',
        meetingLink: 'https://zoom.us/j/1234567890',
        isSubmitted: false,
        isCurrentSession: false,
        sessionNumber: 5,
      },
    ];

    this.oralExaminations$ = examList.map((exam) => {
      return {
        ...exam,
        fullName: `${exam.firstName} ${exam.lastName}`,
        examTime: `${exam.startTime} - ${exam.endTime}`,
        rowClass: this.setExaminationStatus(exam),
      };
    });
  }

  setExaminationStatus(exam: IExamSessionReadOnlyModel) {
    if (exam.isCurrentSession) {
      return 'current-session';
    } else if (exam.isSubmitted) {
      return 'submitted-session';
    } else {
      return 'not-started';
    }
  }

  handleGridAction($event: any) {
    const { data } = $event;
    if ($event.fieldKey === 'startExam') {
      console.log('start exam', data);
      // add any store logic required to start the exam here
      // add any checks to prevent the exam from being started incorrectly here
      // add new global dialog to confirm exam start
      this._globalDialogService
        .showConfirmationWithWarning(
          'Examination Confirmation',
          `Are you sure you want to start the examination for ${data.fullName}?`,
          'Clicking confirm will start the timer and begin the exam.'
        )
        .then((result) => {
          if (result) {
            this._route.navigate([
              'ce-scoring/oral-examinations/exam',
              data.sessionNumber,
            ]);
          }
          // take any actions required on cancel of confirmation here
        });
    } else {
      console.log('unhandled grid action');
    }
  }

  copyFromTextInput(element: any) {
    if (navigator.clipboard) {
      navigator.clipboard.writeText(element.value);
    } else {
      // handle any older browsers that don't support navigator.clipboard
      element.select();
      document.execCommand('copy');
      element.setSelectionRange(0, 0);
    }
  }
}
