import { CUSTOM_ELEMENTS_SCHEMA, Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { GlobalDialogService } from '../shared/services/global-dialog.service';
import { GridComponent } from '../shared/components/grid/grid.component';
import { ORAL_EXAMINATION_COLS } from './oral-examination-cols';

import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { IExamSessionReadOnlyModel } from '../api/models/scoring/exam-session-read-only.model';
import {
  ApplicationSelectors,
  ExamScoringSelectors,
  GetActiveExamId,
  GetExamTitle,
  GetExamineeList,
  IFeatureFlags,
  SkipExam,
} from '../state';
import { Select, Store } from '@ngxs/store';
import { BehaviorSubject, Observable, map, skipWhile } from 'rxjs';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { IExamTitleReadOnlyModel } from '../api/models/examinations/exam-title-read-only.model';
import { LegendComponent } from '../shared/components/legend/legend.component';
import { AlertComponent } from '../shared/components/alert/alert.component';

@UntilDestroy()
@Component({
  selector: 'abs-oral-examinations',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    TranslateModule,
    GridComponent,
    InputTextModule,
    ButtonModule,
    LegendComponent,
    AlertComponent,
  ],
  templateUrl: './oral-examinations.component.html',
  styleUrls: ['./oral-examinations.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class OralExaminationsComponent implements OnInit {
  @Select(ApplicationSelectors.slices.featureFlags) featureFlags$:
    | Observable<IFeatureFlags>
    | undefined;
  @Select(ExamScoringSelectors.slices.examineeList)
  examineeList$: Observable<IExamSessionReadOnlyModel[]> | undefined;

  @Select(ExamScoringSelectors.slices.examTitle) examTitle$:
    | Observable<IExamTitleReadOnlyModel>
    | undefined;

  @Select(ExamScoringSelectors.slices.examHeaderId) examHeaderId$:
    | Observable<number>
    | undefined;

  examDate: Date = new Date();

  examDateDisplay: Date = new Date();
  zoomLink: string | undefined = '';
  oralExaminations$: BehaviorSubject<IExamSessionReadOnlyModel[]> =
    new BehaviorSubject<IExamSessionReadOnlyModel[]>([]);
  oralExaminationCols = ORAL_EXAMINATION_COLS;

  legendItems = [
    {
      text: 'Not Submitted',
      color: '#7f7f7f',
    },
    {
      text: 'Current Session',
      color: '#dbad6a',
    },
    {
      text: 'Submitted',
      color: '#1c827d',
    },
  ];

  constructor(
    private _route: Router,
    private _globalDialogService: GlobalDialogService,
    private _store: Store,
    private _translateService: TranslateService
  ) {
    this.featureFlags$?.pipe().subscribe((featureFlags) => {
      this._store.dispatch(new GetActiveExamId());
      // if (featureFlags) {
      //   if (featureFlags.ceScoreTestingDate) {
      //     this.examDate = new Date('10/16/2023');
      //   }
      // }
    });

    this.examHeaderId$
      ?.pipe(
        skipWhile((examHeaderId) => !examHeaderId),
        untilDestroyed(this)
      )
      .subscribe((examHeaderId) => {
        this._store.dispatch(new GetExamTitle(examHeaderId));
      });
  }

  ngOnInit(): void {
    this.getOralExaminations();
  }

  getOralExaminations() {
    this._store.dispatch(new GetExamineeList(this.examDate.toISOString()));

    this.examineeList$
      ?.pipe(
        untilDestroyed(this),
        map((examineeList) => {
          if (examineeList?.length > 0) {
            return examineeList.map((examinee) => {
              let examTime = 'not scheduled';
              if (examinee.startTime) {
                examTime = `${examinee.startTime}`;
                if (examinee.endTime) {
                  examTime = `${examTime} - ${examinee.endTime}`;
                }
              } else if (examinee.endTime) {
                examTime = `${examinee.endTime}`;
              }
              return {
                ...examinee,
                fullName: `${examinee.firstName} ${examinee.lastName}`,
                examTime: `${examTime}`,
                rowClass: this.setExaminationStatus(examinee),
              };
            });
          }
          return [];
        })
      )
      .subscribe((examineeList) => {
        this.oralExaminations$.next(examineeList);
        if (examineeList?.length > 0) {
          const currentExam = examineeList.find(
            (exam) => exam.isCurrentSession
          );
          if (currentExam && currentExam.meetingLink) {
            this.zoomLink = currentExam.meetingLink;
          } else {
            this.zoomLink = examineeList[0].meetingLink;
          }
        } else {
          this.zoomLink = '';
        }
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
      // add any store logic required to start the exam here
      // add any checks to prevent the exam from being started incorrectly here
      this._globalDialogService
        .showConfirmation(
          this._translateService.instant('EXAMSCORING.EXAMINATION.START.TITLE'),
          this._translateService.instant(
            'EXAMSCORING.EXAMINATION.START.SUBTITLE',
            {
              name: data.fullName,
            }
          )
        )
        .then((result) => {
          if (result) {
            this._route.navigate([
              'ce-scoring/oral-examinations/exam',
              data.examScheduleId,
            ]);
          }
          // take any actions required on cancel of confirmation here
        });
    } else if ($event.fieldKey === 'skipExam') {
      this._globalDialogService
        .showConfirmation(
          this._translateService.instant('EXAMSCORING.EXAMINATION.SKIP.TITLE'),
          this._translateService.instant(
            'EXAMSCORING.EXAMINATION.SKIP.SUBTITLE'
          )
        )
        .then((result) => {
          if (result) {
            this._store.dispatch(
              new SkipExam(
                $event.data.examScheduleId,
                this.examDate.toISOString()
              )
            );
          }
        });
    } else {
      console.log('unhandled grid action', $event);
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

  openZoomLink() {
    if (this.zoomLink) {
      window.open(this.zoomLink, '_blank');
    }
  }
}
