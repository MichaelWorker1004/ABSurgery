import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { ExaminationScoreCardComponent } from 'src/app/shared/components/examination-score-card/examination-score-card.component';
import { BehaviorSubject, isObservable } from 'rxjs';
import { Store } from '@ngxs/store';
import {
  CreateCaseScore,
  CreateExamScore,
  UpdateCaseScore,
} from 'src/app/state';
import { ICaseScoreModel } from 'src/app/api';
import { Status } from './status.enum';
import { IExamScoreModel } from 'src/app/api/models/ce/exam-score.model';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

@UntilDestroy()
@Component({
  selector: 'abs-examination-score-modal',
  standalone: true,
  imports: [CommonModule, ButtonModule, ExaminationScoreCardComponent],
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

    this.selectedStatusOption.subscribe((status: string) => {
      this.examStatus = status;
    });
  }

  handleChange(event: any) {
    this.candidateCaseScores[event.case.examCaseId] = { ...event.case };

    const scores: number[] = [];

    Object.entries(this.candidateCaseScores).forEach(([key, value]) => {
      const data = value as ICaseScoreModel;
      if (data?.score) {
        scores.push(data?.score);
      }
    });

    if (scores.length === this.examLength) {
      this.disableSubmitExamScore = false;
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
      .subscribe(() => {
        this.resetData();
        this.closeDialog.emit(true);
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
        score: data?.score,
        criticalFail: data?.criticalFail,
        remarks: data?.remarks,
      } as ICaseScoreModel;

      if (data?.examScoringId) {
        promises.push(
          this._store.dispatch(new UpdateCaseScore(model)).toPromise()
        );

        this.resetData();
        this.closeDialog.emit();
      } else {
        promises.push(
          this._store.dispatch(new CreateCaseScore(model)).toPromise()
        );
      }
    });

    await Promise.all(promises);

    if (closeDialog) {
      this.resetData();
      this.closeDialog.emit();
    }
  }

  cancel() {
    this.resetData();
    this.closeDialog.emit();
  }

  resetData() {
    this.disableSubmitExamScore = true;
    this.candidateCaseScores = {};
    this.localCandidateData = {};
    this.candidateCaseScores = {};
  }
}
