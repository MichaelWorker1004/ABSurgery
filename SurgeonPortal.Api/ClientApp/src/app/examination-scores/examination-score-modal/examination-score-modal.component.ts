import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { ExaminationScoreCardComponent } from 'src/app/shared/components/examination-score-card/examination-score-card.component';
import { isObservable } from 'rxjs';
import { Store } from '@ngxs/store';
import { UpdateCaseScore } from 'src/app/state';
import { ICaseScoreModel } from 'src/app/api';

@Component({
  selector: 'abs-examination-score-modal',
  standalone: true,
  imports: [CommonModule, ButtonModule, ExaminationScoreCardComponent],
  templateUrl: './examination-score-modal.component.html',
  styleUrls: ['./examination-score-modal.component.scss'],
})
export class ExaminationScoreModalComponent implements OnInit {
  @Input() candidateData: any;
  @Output() closeDialog = new EventEmitter();

  candidateCaseScores = {} as any;
  localCandidateData = {} as any;

  constructor(private _store: Store) {}

  ngOnInit(): void {
    if (isObservable(this.candidateData)) {
      this.candidateData.subscribe((data) => {
        this.localCandidateData = data;
      });
    } else {
      this.localCandidateData = this.candidateData;
    }
  }

  handleChange(event: any) {
    this.candidateCaseScores[event.case.examCaseId] = { ...event.case };
  }

  saveScores() {
    console.log(this.candidateCaseScores);

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
      this._store.dispatch(new UpdateCaseScore(model));
    });
  }

  cancel() {
    this.closeDialog.emit();
    this.localCandidateData = {};
    this.candidateCaseScores = {};
  }
}
