import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { ExaminationScoreCardComponent } from 'src/app/shared/components/examination-score-card/examination-score-card.component';
import { BehaviorSubject, isObservable } from 'rxjs';
import { Store } from '@ngxs/store';
import {
  ClearExamScoringErrors,
  CreateCaseScore,
  CreateExamScore,
  UpdateCaseScore,
} from 'src/app/state';
import { ICaseScoreModel } from 'src/app/api';
import { Status } from './status.enum';
import { IExamScoreModel } from 'src/app/api/models/ce/exam-score.model';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { IFormErrors } from 'src/app/shared/common';
import { FormErrorsComponent } from 'src/app/shared/components/form-errors/form-errors.component';

@UntilDestroy()
@Component({
  selector: 'abs-examination-score-modal',
  standalone: true,
  imports: [
    CommonModule,
    ButtonModule,
    ExaminationScoreCardComponent,
    FormErrorsComponent,
  ],
  templateUrl: './examination-score-modal.component.html',
  styleUrls: ['./examination-score-modal.component.scss'],
})
export class ExaminationScoreModalComponent implements OnInit {
  @Input() candidateData: any;
  @Input() selectedStatusOption: any;
  @Input() examScheduleId!: any;
  @Output() closeDialog = new EventEmitter();

  candidateCaseScores = {} as any;
  localCandidateData = {} as any;

  Status = Status;
  examStatus!: string;

  examLength!: number;

  disableSubmitExamScore = true;

  scoredExams = 0;

  errors: IFormErrors | null = null;
  clearErrors = new ClearExamScoringErrors();

  constructor(private _store: Store) {}

  ngOnInit(): void {
    this.localCandidateData = {};
    if (isObservable(this.candidateData)) {
      this.candidateData.pipe(untilDestroyed(this)).subscribe((data: any) => {
        this.localCandidateData = data;
        this.examLength = data?.cases?.length;
      });
    } else {
      this.localCandidateData = this.candidateData;
    }

    this.selectedStatusOption?.subscribe((status: string) => {
      this.examStatus = status;
    });
  }

  handleChange(event: any) {
    this.candidateCaseScores[event.case.examCaseId] = { ...event.case };

    const scores: number[] = [];

    Object.entries(this.candidateCaseScores).forEach(([key, value]) => {
      const data = value as ICaseScoreModel;
      if (data?.score && data.score > 0) {
        scores.push(data?.score);
      }
    });

    if (scores.length === this.examLength) {
      this.disableSubmitExamScore = false;
    } else {
      this.disableSubmitExamScore = true;
    }
  }

  async submitScores() {
    await this.saveScores();
    const model = {
      examScheduleId: this.examScheduleId,
    } as IExamScoreModel;

    this._store
      .dispatch(new CreateExamScore(model, false))
      .pipe(untilDestroyed(this))
      .subscribe((results) => {
        if (results.examScoring.examErrors) {
          this.errors = results.examScoring.examErrors;
          const element = document.getElementById(
            'exam-score-errors-container'
          );
          if (element) {
            element.focus();
            element.scrollIntoView({
              behavior: 'smooth',
            });
          }
        } else {
          this.errors = null;
          this.resetData();
          this.closeDialog.emit(true);
        }
      });
  }

  async saveScores(closeDialog = false) {
    const promises: Promise<any>[] = [];

    Object.entries(this.candidateCaseScores).forEach(([key, value]) => {
      const data = value as ICaseScoreModel;
      const model = {
        examCaseId: data?.examCaseId,
        examScoringId: data?.examScoringId,
        examinerUserId: data?.examinerUserId,
        examineeUserId: data?.examineeUserId,
        score: data?.score ?? 0,
        criticalFail: data?.criticalFail,
        remarks: data?.remarks,
      } as ICaseScoreModel;

      if (model?.examScoringId) {
        promises.push(
          this._store.dispatch(new UpdateCaseScore(model, false)).toPromise()
        );
      } else if ((model?.score && model.score > 0) || model?.remarks) {
        promises.push(
          this._store.dispatch(new CreateCaseScore(model)).toPromise()
        );
      }
    });

    await Promise.all(promises).then(() => {
      if (closeDialog) {
        this.resetData();
        this.closeDialog.emit();
      }
    });
  }

  cancel() {
    this.resetData();
    this.closeDialog.emit();
  }

  resetData() {
    this.errors = null;
    this.disableSubmitExamScore = true;
    this.candidateCaseScores = {};
    this.localCandidateData = {};
    this.candidateCaseScores = {};
  }
}
