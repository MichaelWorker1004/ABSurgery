import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  OnInit,
  ViewChild,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { ExpandableComponent } from '../shared/components/expandable/expandable.component';
import { ButtonModule } from 'primeng/button';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { ExaminationScoreCardComponent } from '../shared/components/examination-score-card/examination-score-card.component';
import { BehaviorSubject, Observable } from 'rxjs';
import { Select, Store } from '@ngxs/store';
import {
  CreateCaseScore,
  CreateExamScore,
  ExamScoringSelectors,
  GetExaminee,
  GetSelectedExamScores,
  UpdateCaseScore,
  UserProfileSelectors,
} from '../state';
import { IExamineeReadOnlyModel } from '../api/models/scoring/ce/examinee-read-only.model';
import { ExamTimerComponent } from '../shared/components/exam-timer-component/exam-timer.component';
import { ICaseScoreModel, ICaseScoreReadOnlyModel } from '../api';
import { IExamScoreModel } from '../api/models/ce/exam-score.model';

@Component({
  selector: 'abs-oral-examination',
  standalone: true,
  imports: [
    CommonModule,
    ExpandableComponent,
    ButtonModule,
    InputTextareaModule,
    ExaminationScoreCardComponent,
    ExamTimerComponent,
  ],
  templateUrl: './oral-examination.component.html',
  styleUrls: ['./oral-examination.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class OralExaminationsComponent implements OnInit {
  @Select(ExamScoringSelectors.slices.examinee) examinee$:
    | Observable<IExamineeReadOnlyModel>
    | undefined;

  @Select(UserProfileSelectors.slices.userId) userId$:
    | Observable<number>
    | undefined;

  @Select(ExamScoringSelectors.slices.selectedExamScores) gradedScores$:
    | Observable<ICaseScoreReadOnlyModel[]>
    | undefined;

  @ViewChild(ExamTimerComponent) ExamTimerComponent!: ExamTimerComponent;

  cases$: BehaviorSubject<any> = new BehaviorSubject([]);
  userId!: number;
  casesLenth!: number;
  currentYear = new Date().getFullYear();
  examinationId!: string | null;
  candidateName!: string;
  examScoringId!: number;
  examineeUserId!: number;
  examScheduleId!: number;
  dayTime!: string;
  activeCase = 0;
  currentIncrement = 1;
  scores!: any[];
  candidateCaseScores = {} as any;
  gradedCandidateCaseCores = {} as any;
  candidateScores: any[] = [];
  showTimer = false;
  disable = true;
  submitDisable = true;

  constructor(private activatedRoute: ActivatedRoute, private _store: Store) {}

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params) => {
      this.examScheduleId = params['examinationId'];
      this._store.dispatch(new GetExaminee(params['examinationId']));
    });

    this.userId$?.subscribe((userId: number) => {
      this.userId = userId;
    });

    this.getExaminationData();
  }

  getExaminationData() {
    this.examinee$?.subscribe((examinee: IExamineeReadOnlyModel) => {
      if (examinee) {
        this.candidateName = examinee?.fullName;
        this.dayTime = examinee?.examDate;
        this.cases$.next(examinee.cases);
        this.casesLenth = examinee.cases?.length;
        this.examScoringId = examinee?.examScoringId;
        this.examineeUserId = examinee?.examineeUserId;
        this.showTimer = true;
      }
    });
  }

  handleChange(event: any) {
    if (event.case.score) {
      this.candidateCaseScores[event.case.examCaseId] = event.case;
      this.disable = false;
    }
  }

  handleGradedChange(event: any) {
    this.gradedCandidateCaseCores[event.case.examCaseId] = { ...event.case };
    this.submitDisable = false;
  }

  handleSave(examCaseId: number) {
    const currentCase = this.candidateCaseScores[examCaseId];

    const model = {
      examCaseId: currentCase?.examCaseId,
      examinerUserId: this.userId,
      examineeUserId: this.examineeUserId,
      score: +currentCase?.score,
      criticalFail: currentCase?.criticalFail ?? false,
      remarks: currentCase?.remarks ?? '',
    } as ICaseScoreModel;

    this._store.dispatch(new CreateCaseScore(model));

    this.activeCase += 1;
    this.currentIncrement += 1;
    this.disable = true;

    if (this.currentIncrement > this.casesLenth) {
      this._store.dispatch(new GetSelectedExamScores(this.examScheduleId));
      this.ExamTimerComponent.stopTimers();
    }
  }

  handleSaveAndSubmitLater() {
    this.updateScores();
  }

  handleSubmit() {
    this.updateScores();
    const model = {
      examScheduleId: this.examScheduleId,
    } as IExamScoreModel;

    this._store.dispatch(new CreateExamScore(model));
  }

  updateScores() {
    if (Object.entries(this.gradedCandidateCaseCores).length > 0) {
      Object.entries(this.gradedCandidateCaseCores).forEach(([key, value]) => {
        const data = value as ICaseScoreModel;
        const model = {
          examCaseId: data?.examCaseId,
          examScoringId: data?.examScoringId,
          examinerUserId: data?.examinerUserId,
          examineeUserId: data?.examineeUserId,
          score: +data?.score,
          criticalFail: data?.criticalFail,
          remarks: data?.remarks,
        } as ICaseScoreModel;
        this._store.dispatch(new UpdateCaseScore(model, false));
      });
    }
  }
}
