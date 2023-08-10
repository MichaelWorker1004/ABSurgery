import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  HostListener,
  OnInit,
  ViewChild,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
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
  GetExamTitle,
  GetExaminee,
  GetSelectedExamScores,
  UpdateCaseScore,
  UserProfileSelectors,
} from '../state';
import { IExamineeReadOnlyModel } from '../api/models/scoring/ce/examinee-read-only.model';
import { ExamTimerComponent } from '../shared/components/exam-timer-component/exam-timer.component';
import { ICaseScoreModel, ICaseScoreReadOnlyModel } from '../api';
import { IExamScoreModel } from '../api/models/ce/exam-score.model';
import { GlobalDialogService } from '../shared/services/global-dialog.service';
import { IExamTitleReadOnlyModel } from '../api/models/examinations/exam-title-read-only.model';

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
  @HostListener('contextmenu', ['$event'])
  onRightClick(event: any) {
    event.preventDefault();
  }

  @Select(ExamScoringSelectors.slices.examinee) examinee$:
    | Observable<IExamineeReadOnlyModel>
    | undefined;

  @Select(UserProfileSelectors.slices.userId) userId$:
    | Observable<number>
    | undefined;

  @Select(ExamScoringSelectors.slices.selectedExamScores) gradedScores$:
    | Observable<ICaseScoreReadOnlyModel[]>
    | undefined;

  @Select(ExamScoringSelectors.slices.examTitle) examTitle$:
    | Observable<IExamTitleReadOnlyModel>
    | undefined;

  @ViewChild(ExamTimerComponent) ExamTimerComponent!: ExamTimerComponent;

  examHeaderId = 491; // TODO - remove hard coded value

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

  hasUnsavedChanges = true;

  constructor(
    private activatedRoute: ActivatedRoute,
    private _store: Store,
    private router: Router,
    public globalDialogService: GlobalDialogService
  ) {
    this._store.dispatch(new GetExamTitle(this.examHeaderId));
  }

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
      this.ExamTimerComponent.stopTimers();
      this.globalDialogService.showLoading();
      setTimeout(() => {
        this._store.dispatch(new GetSelectedExamScores(this.examScheduleId));
        this.disable = true;
      }, 200);
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

    this.hasUnsavedChanges = false;
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
        this.hasUnsavedChanges = false;
        this._store.dispatch(new UpdateCaseScore(model, false));
        this.globalDialogService.showSuccessError(
          'Success',
          'Scores updated successfully',
          true
        );
        this.router.navigate(['/ce-scoring/oral-examinations']);
      });
    }
  }
}
