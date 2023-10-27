import { CommonModule } from '@angular/common';
import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  HostListener,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { TranslateModule } from '@ngx-translate/core';
import { Select, Store } from '@ngxs/store';
import { ButtonModule } from 'primeng/button';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { BehaviorSubject, Observable, distinctUntilChanged, take } from 'rxjs';
import { ICaseScoreModel, ICaseScoreReadOnlyModel } from '../api';
import { IExamScoreModel } from '../api/models/ce/exam-score.model';
import { IExamTitleReadOnlyModel } from '../api/models/examinations/exam-title-read-only.model';
import { IExamineeReadOnlyModel } from '../api/models/scoring/ce/examinee-read-only.model';
import { IFormErrors } from '../shared/common';
import { ExamTimerComponent } from '../shared/components/exam-timer-component/exam-timer.component';
import { ExaminationScoreCardComponent } from '../shared/components/examination-score-card/examination-score-card.component';
import { ExpandableComponent } from '../shared/components/expandable/expandable.component';
import { FormErrorsComponent } from '../shared/components/form-errors/form-errors.component';
import { GlobalDialogService } from '../shared/services/global-dialog.service';
import {
  ApplicationSelectors,
  ClearExamScoringErrors,
  ClearExamineeData,
  CreateCaseScore,
  CreateExamScore,
  ExamScoringSelectors,
  GetExamHeaderId,
  GetExamTitle,
  GetExaminee,
  GetSelectedExamScores,
  IFeatureFlags,
  SkipExam,
  UpdateCaseScore,
  UserProfileSelectors,
} from '../state';
import { SetExamInProgress } from '../state/application/application.actions';
import { s } from '@fullcalendar/core/internal-common';

@UntilDestroy()
@Component({
  selector: 'abs-oral-examination',
  standalone: true,
  imports: [
    CommonModule,
    TranslateModule,
    ExpandableComponent,
    ButtonModule,
    InputTextareaModule,
    ExaminationScoreCardComponent,
    ExamTimerComponent,
    FormErrorsComponent,
  ],
  templateUrl: './oral-examination.component.html',
  styleUrls: ['./oral-examination.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class OralExaminationsComponent implements OnInit, OnDestroy {
  @HostListener('contextmenu', ['$event'])
  onRightClick(event: any) {
    event.preventDefault();
  }

  @Select(ApplicationSelectors.slices.featureFlags) featureFlags$:
    | Observable<IFeatureFlags>
    | undefined;

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

  @Select(ExamScoringSelectors.slices.examHeaderId) examHeaderId$:
    | Observable<number>
    | undefined;

  @ViewChild(ExamTimerComponent) ExamTimerComponent!: ExamTimerComponent;

  cases$: BehaviorSubject<any> = new BehaviorSubject([]);
  userId!: number;
  casesLength!: number;
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

  disableSubmit = true;

  errors: IFormErrors | null = null;
  clearErrors = new ClearExamScoringErrors();

  constructor(
    private activatedRoute: ActivatedRoute,
    private _store: Store,
    private router: Router,
    public globalDialogService: GlobalDialogService
  ) {
    this.featureFlags$?.pipe(untilDestroyed(this)).subscribe((featureFlags) => {
      if (featureFlags?.ceScoreTesting) {
        this._store.dispatch(new GetExamHeaderId(featureFlags.ceScoreTesting));
      }
    });

    this.examHeaderId$?.pipe(untilDestroyed(this)).subscribe((examHeaderId) => {
      this._store.dispatch(new GetExamTitle(examHeaderId));
    });

    window.onbeforeunload = (e) => {
      e.returnValue = true;
    };
  }
  ngOnDestroy(): void {
    this._store.dispatch(new SetExamInProgress(false));
  }

  ngOnInit(): void {
    this.globalDialogService.showLoading();
    this._store.dispatch(new SetExamInProgress(true));

    this.activatedRoute.params.subscribe((params) => {
      this.examScheduleId = params['examinationId'];

      this._store
        .dispatch(new ClearExamineeData())
        .pipe(untilDestroyed(this))
        .subscribe(() => {
          this._store
            .dispatch(new GetExaminee(params['examinationId']))
            .pipe(take(1))
            .subscribe(() => {
              this.userId$
                ?.pipe(untilDestroyed(this))
                .subscribe((userId: number) => {
                  this.userId = userId;
                });

              this.getExaminationData();
            });
        });
    });
  }

  getExaminationData() {
    this.examinee$
      ?.pipe(untilDestroyed(this), distinctUntilChanged())
      .subscribe((examinee: IExamineeReadOnlyModel) => {
        if (examinee) {
          this.setActiveExam(examinee.cases);
          this.candidateName = examinee?.fullName;
          this.dayTime = examinee?.examDate;
          this.cases$.next(examinee.cases);
          this.casesLength = examinee.cases?.length;
          this.examScoringId = examinee?.examScoringId;
          this.examineeUserId = examinee?.examineeUserId;
          setTimeout(() => {
            this.globalDialogService.closeOpenDialog();
          }, 0);
          this.showTimer = true;
        }
      });
  }

  setActiveExam(cases: any[]) {
    const ungradedCase = cases.findIndex((c) => !c.score);

    if (ungradedCase !== 0) {
      if (ungradedCase !== -1) {
        this.activeCase = ungradedCase;
        this.currentIncrement = ungradedCase + 1;
      } else {
        this.activeCase = cases.length;
        this.currentIncrement = cases.length + 1;
        this.setReviewExpand();
      }
    }
  }

  handleChange(event: any) {
    if (event.case.score || event.case.remarks) {
      this.candidateCaseScores[event.case.examCaseId] = event.case;
      if (event.case.score && event.case.score > 0) {
        this.disable = false;
      }
    }
  }

  handleGradedChange(event: any) {
    this.gradedCandidateCaseCores[event.case.examCaseId] = { ...event.case };

    const scores: number[] = [];

    Object.entries(this.gradedCandidateCaseCores).forEach(([key, value]) => {
      const data = value as ICaseScoreModel;
      if (data?.score > 0) {
        scores.push(data?.score);
      }
    });

    setTimeout(() => {
      if (scores.length === this.casesLength) {
        this.disableSubmit = false;
      }
    }, 0);
  }

  handleNextCase() {
    this.activeCase += 1;
    this.currentIncrement += 1;
    this.disable = true;

    if (this.currentIncrement === this.casesLength) {
      this.setReviewExpand();
    } else {
      this.scrollToElementById(
        'expandableHeader' + (this.currentIncrement - 1)
      );
    }
  }

  setReviewExpand() {
    this._store
      .dispatch(new GetSelectedExamScores(this.examScheduleId))
      .pipe(untilDestroyed(this))
      .subscribe(() => {
        this.scrollToElementById('expandableHeader' + this.casesLength);
        this.ExamTimerComponent?.stopTimers();

        this.disable = true;
      });
  }

  handleSave(examCaseId: number, skipped = false) {
    const currentCase = this.candidateCaseScores[examCaseId];

    const model = {
      examCaseId: currentCase?.examCaseId,
      examinerUserId: this.userId,
      examineeUserId: this.examineeUserId,
      score: !skipped ? currentCase?.score ?? 0 : 0,
      criticalFail: currentCase?.criticalFail ?? false,
      remarks: currentCase?.remarks ?? '',
    } as ICaseScoreModel;

    if ((model?.score && model.score > 0) || model?.remarks) {
      this._store
        .dispatch(new CreateCaseScore(model))
        .pipe(untilDestroyed(this))
        .subscribe(() => {
          this._store.dispatch(new GetSelectedExamScores(this.examScheduleId));
          this.handleNextCase();
        });
    } else {
      this.handleNextCase();
    }
  }

  skipCase(examCaseId: number) {
    let message = 'Skip scoring for now.  (I will score later.)';
    const currentCase = this.candidateCaseScores[examCaseId];
    if (currentCase?.score > 0) {
      message =
        'Skip scoring for now.  (I will score later.) <br> This will remove the selected case score.';
    }
    this.globalDialogService
      ?.showConfirmation('Skip Case', message)
      .then((result) => {
        if (result) {
          this.handleSave(examCaseId, true);
        }
      });
  }

  scrollToElementById(elementId: string) {
    const element = document.getElementById(elementId);
    if (element) {
      setTimeout(() => {
        element.scrollIntoView({ behavior: 'smooth' });
      }, 0);
    }
  }

  async handleSaveAndSubmitLater() {
    await this.updateScores();

    this._store.dispatch(new SetExamInProgress(false));

    const dateParts = this.dayTime.replace(/\s+/g, ' ').trim().split(' ');
    const formattedDate = `${dateParts[1]} ${dateParts[0]} ${dateParts[2]}`;
    const examDate = new Date(formattedDate);

    this._store
      .dispatch(new SkipExam(this.examScheduleId, examDate.toISOString()))
      .pipe(take(1))
      .subscribe(() => {
        this.router.navigate(['/ce-scoring/oral-examinations']);
      });
  }

  async handleSubmit() {
    this.globalDialogService.showLoading();
    await this.updateScores();
    const model = {
      examScheduleId: this.examScheduleId,
    } as IExamScoreModel;

    this._store.dispatch(new SetExamInProgress(false));

    let currentCase = 0;

    if (Object.entries(this.gradedCandidateCaseCores).length > 0) {
      Object.entries(this.gradedCandidateCaseCores).forEach(() => {
        currentCase += 1;
      });
    }
    this._store
      .dispatch(new CreateExamScore(model, false))
      .pipe(take(1))
      .subscribe((results) => {
        if (results.examScoring.examErrors) {
          // handle exam submission error
          this.errors = results.examScoring.examErrors;
          this.scrollToElementById('exam-score-errors');
        } else {
          this.errors = null;
          this.router.navigate(['/ce-scoring/oral-examinations']);
        }
      });
  }

  async updateScores() {
    const promise: Promise<any>[] = [];
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
        this._store.dispatch(new SetExamInProgress(false));

        if (data?.examScoringId) {
          promise.push(
            this._store.dispatch(new UpdateCaseScore(model, false)).toPromise()
          );
        } else if ((model?.score && model.score > 0) || model?.remarks) {
          promise.push(
            this._store.dispatch(new CreateCaseScore(model, false)).toPromise()
          );
        }
      });
    }

    await Promise.all(promise);
  }
}
