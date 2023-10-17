import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { catchError, map, share, tap } from 'rxjs/operators';
import { Observable, forkJoin, of } from 'rxjs';
import { Action, State, StateContext, StateToken } from '@ngxs/store';
import {
  CaseContentsService,
  CaseNotesService,
  CasesService,
  ExamSessionsService,
  ICaseCommentModel,
  ICaseDetailReadOnlyModel,
  ICaseRosterReadOnlyModel,
  IExamSessionReadOnlyModel,
  CaseScoresService,
  ICaseScoreReadOnlyModel,
  ICaseScoreModel,
} from '../../api';
import { IFormErrors } from '../../shared/common';
import {
  GetCaseRoster,
  GetCaseContents,
  GetCaseComment,
  CreateCaseComment,
  UpdateCaseComment,
  GetExamineeList,
  GetExamScoresList,
  GetSelectedExamScores, // if no api call is needed create a custom selector for this
  ClearExamScoringErrors,
  GetRoster,
  CreateCaseScore,
  UpdateCaseScore,
  DeleteCaseScore,
  GetExaminee,
  CreateExamScore,
  DeleteCaseComment,
  SkipExam,
  ResetCaseCommentsData,
  ResetExamScoringData,
  GetExamTitle,
  CreateCaseFeedback,
  GetCaseFeedback,
  UpdateCaseFeedback,
  DeleteCaseFeedback,
  GetCaseDetailsAndFeedback,
  GetExaminerAgenda,
  GetExaminerConflict,
  GetExamHeaderId,
} from './exam-scoring.actions';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';
import { RostersService } from 'src/app/api/services/scoring/rosters.service';
import { IRosterReadOnlyModel } from 'src/app/api/models/scoring/roster-read-only.model';
import { DashboardService } from 'src/app/api/services/scoring/dashboard.service';
import { IDashboardRosterReadOnlyModel } from 'src/app/api/models/scoring/dashboard-roster-read-only.model';
import { IExamineeReadOnlyModel } from 'src/app/api/models/scoring/ce/examinee-read-only.model';
import { SessionService } from 'src/app/api/services/scoring/ce/session.service';
import { ExamScoreService } from 'src/app/api/services/ce/exam-score.service';
import { IExamScoreModel } from 'src/app/api/models/ce/exam-score.model';
import { Router } from '@angular/router';
import { ExaminationsService } from 'src/app/api/services/examinations/examinations.service';
import { IExamTitleReadOnlyModel } from 'src/app/api/models/examinations/exam-title-read-only.model';
import { ICaseFeedbackModel } from 'src/app/api/models/scoring/case-feedback.model';
import { CaseFeedbackService } from 'src/app/api/services/scoring/case-feedback.service';
import { IAgendaReadOnlyModel } from 'src/app/api/models/examiners/agenda-read-only.model';
import { AgendaService } from 'src/app/api/services/examiners/agenda.service';
import { ConflictService } from 'src/app/api/services/examiners/conflict.service';
import { IConflictReadOnlyModel } from 'src/app/api/models/examiners/conflict-read-only.model';
import { ICaseFeedbackReadOnlyModel } from 'src/app/api/models/scoring/case-feedback-read-only.model';

export interface IExamScoring {
  examTitle: IExamTitleReadOnlyModel | undefined;
  // examination rosters page values
  caseRoster: ICaseRosterReadOnlyModel[] | undefined; // examination rosters page list values
  selectedCaseContents: ICaseDetailReadOnlyModel[] | undefined; // examination rosters page details values
  selectedCaseComment: ICaseCommentModel | undefined; // examination rosters page selected comment value
  selectedCaseFeedback: ICaseFeedbackReadOnlyModel | undefined; // examination rosters page selected feedback value
  // oral-examinations list page values
  examineeList: IExamSessionReadOnlyModel[] | undefined; // oral-examinations list page grid values
  // oral-examination actual exam page values
  activeExamination: any[] | undefined; // oral-examination actual exam (includes all cases for selected exam) (no api call)
  //Daily Exam Scores page values
  examScoresList: IRosterReadOnlyModel[] | undefined; // Daily Exam Scores page grid values
  selectedExamScores: ICaseScoreReadOnlyModel[] | undefined; // Daily Exam Scores page details values (no api call)
  // misc values

  roster: IRosterReadOnlyModel[] | undefined;
  dashboardRoster: IDashboardRosterReadOnlyModel[] | undefined;
  examinee: IExamineeReadOnlyModel | undefined;
  examinerAgenda: IAgendaReadOnlyModel | undefined;
  examinerConflict: IConflictReadOnlyModel | undefined;
  errors: IFormErrors | null;
  examErrors: IFormErrors | null;
  examHeaderId: number | null;
}

export const EXAM_SCORING_STATE_TOKEN = new StateToken<IExamScoring>(
  'examScoring'
);

@State<IExamScoring>({
  name: EXAM_SCORING_STATE_TOKEN,
  defaults: {
    examTitle: undefined,
    caseRoster: undefined,
    selectedCaseContents: undefined,
    selectedCaseComment: undefined,
    examScoresList: undefined,
    selectedExamScores: undefined,
    examineeList: undefined,
    activeExamination: undefined,
    roster: undefined,
    dashboardRoster: undefined,
    examinee: undefined,
    selectedCaseFeedback: undefined,
    examinerAgenda: undefined,
    examinerConflict: undefined,
    errors: null,
    examErrors: null,
    examHeaderId: 482,
  },
})
@Injectable()
export class ExamScoringState {
  examDate!: string;

  constructor(
    private casesService: CasesService,
    private caseContentsService: CaseContentsService,
    private caseCommentsService: CaseNotesService,
    private examScoreService: ExamScoreService,
    private rostersService: RostersService,
    private examSessionsService: ExamSessionsService,
    private dashboardService: DashboardService,
    private caseScoresService: CaseScoresService,
    private sessionService: SessionService,
    private examinationsService: ExaminationsService,
    private globalDialogService: GlobalDialogService,
    private caseFeedbackService: CaseFeedbackService,
    private agendaService: AgendaService,
    private conflictSerive: ConflictService,
    private router: Router
  ) {}

  @Action(GetExamTitle)
  getExamTitle(ctx: StateContext<IExamScoring>, payload: { id: number }) {
    // const state = ctx.getState();
    const sessionId = payload.id;

    if (ctx.getState()?.examTitle) {
      return of(ctx.getState()?.examTitle);
    }
    return this.examinationsService
      .retrieveExamTitleReadOnly_GetByExamId(sessionId)
      .pipe(
        tap((result: IExamTitleReadOnlyModel) => {
          ctx.patchState({
            examTitle: result,
            errors: null,
            examErrors: null,
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({ errors });
          return of(errors);
        })
      );
  }

  @Action(GetCaseRoster)
  getExamCases(
    ctx: StateContext<IExamScoring>,
    payload: { id1: number; id2?: number }
  ) {
    // const state = ctx.getState();
    const sessionId1 = payload.id1;
    const sessionId2 = payload.id2 || undefined;
    return this.casesService
      .retrieveCaseRosterReadOnly_GetByScheduleId(sessionId1, sessionId2)
      .pipe(
        tap((result: ICaseRosterReadOnlyModel[]) => {
          ctx.patchState({
            caseRoster: result,
            errors: null,
            examErrors: null,
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({ errors });
          return of(errors);
        })
      );
  }

  @Action(GetCaseContents)
  getCaseContents(ctx: StateContext<IExamScoring>, payload: { id: number }) {
    // const state = ctx.getState();
    const caseId = payload.id;
    return this.caseContentsService
      .retrieveCaseDetailReadOnly_GetByCaseHeaderId(caseId)
      .pipe(
        tap((result: ICaseDetailReadOnlyModel[]) => {
          ctx.patchState({
            selectedCaseContents: result,
            errors: null,
            examErrors: null,
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({ errors });
          return of(errors);
        })
      );
  }

  @Action(GetCaseComment)
  getCaseComment(ctx: StateContext<IExamScoring>, payload: { id: number }) {
    // const state = ctx.getState();
    const commentId = payload.id;
    return this.caseCommentsService.retrieveCaseComment_GetById(commentId).pipe(
      tap((result: ICaseCommentModel) => {
        ctx.patchState({
          selectedCaseComment: result,
          errors: null,
          examErrors: null,
        });
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({ errors });
        return of(errors);
      })
    );
  }

  @Action(CreateCaseComment)
  createCaseComment(
    ctx: StateContext<IExamScoring>,
    payload: { comment: ICaseCommentModel }
  ) {
    const comment = payload.comment;
    return this.caseCommentsService.createCaseComment(comment).pipe(
      tap((result: ICaseCommentModel) => {
        // action does not currently update value of selectedCaseConents, relying on UI to refresh as needed
        ctx.patchState({
          selectedCaseComment: result,
          errors: null,
          examErrors: null,
        });
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({ errors });
        return of(errors);
      })
    );
  }

  @Action(UpdateCaseComment)
  updateCaseComment(
    ctx: StateContext<IExamScoring>,
    payload: { comment: ICaseCommentModel }
  ) {
    const comment = payload.comment;
    return this.caseCommentsService.updateCaseComment(comment.id, comment).pipe(
      tap((result: ICaseCommentModel) => {
        // action does not currently update value of selectedCaseConents, relying on UI to refresh as needed
        ctx.patchState({
          selectedCaseComment: result,
          errors: null,
          examErrors: null,
        });
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({ errors });
        return of(errors);
      })
    );
  }

  @Action(DeleteCaseComment)
  deleteCaseComment(ctx: StateContext<IExamScoring>, payload: { id: number }) {
    const commentId = payload.id;
    return this.caseCommentsService.deleteCaseComment(commentId).pipe(
      tap((result: ICaseCommentModel) => {
        // action does not currently update value of selectedCaseConents, relying on UI to refresh as needed
        ctx.patchState({
          selectedCaseComment: result,
          errors: null,
          examErrors: null,
        });
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({ errors });
        return of(errors);
      })
    );
  }

  @Action(GetExamineeList)
  getExamineeList(ctx: StateContext<IExamScoring>, payload: { date: string }) {
    // const state = ctx.getState();
    const date = payload.date;
    return this.examSessionsService
      .retrieveExamSessionReadOnly_GetByUserId(date)
      .pipe(
        tap((result: IExamSessionReadOnlyModel[]) => {
          ctx.patchState({
            examineeList: result,
            errors: null,
            examErrors: null,
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({ errors });
          return of(errors);
        })
      );
  }

  @Action(GetExaminee)
  getExaminee(
    ctx: StateContext<IExamScoring>,
    payload: { examScheduleId: number }
  ) {
    const examScheduleId = payload.examScheduleId;
    this.globalDialogService.showLoading();
    return this.sessionService
      .retrieveExamineeReadOnly_GetById(examScheduleId)
      .pipe(
        tap((examinee: IExamineeReadOnlyModel) => {
          ctx.patchState({
            examinee,
            errors: null,
            examErrors: null,
          });
          this.globalDialogService.closeOpenDialog();
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({ errors });
          this.globalDialogService.closeOpenDialog();
          return of(errors);
        })
      );
  }

  @Action(GetExamScoresList)
  getExamScoresList(ctx: StateContext<IExamScoring>, payload: { id: number }) {
    // const state = ctx.getState();
    const examHeaderId = payload.id;
    return this.rostersService
      .retrieveRosterReadOnly_GetByExaminationHeaderId(examHeaderId)
      .pipe(
        tap((result: IRosterReadOnlyModel[]) => {
          ctx.patchState({
            examScoresList: result,
            errors: null,
            examErrors: null,
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({ errors });
          return of(errors);
        })
      );
  }

  @Action(CreateCaseScore)
  createCaseScore(
    ctx: StateContext<IExamScoring>,
    payload: { score: ICaseScoreModel; showLoading: boolean }
  ) {
    const score = payload.score;
    if (payload.showLoading) {
      this.globalDialogService.showLoading();
    }

    return this.caseScoresService.createCaseScore(score).pipe(
      tap(() => {
        // figure out how to update the store here
        ctx.patchState({
          // selectedCaseComment: result,
          errors: null,
          examErrors: null,
        });
        if (payload.showLoading) {
          this.globalDialogService.closeOpenDialog();
        }
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({ errors });
        if (payload.showLoading) {
          this.globalDialogService.closeOpenDialog();
        }
        return of(errors);
      })
    );
  }

  @Action(UpdateCaseScore)
  updateCaseScore(
    ctx: StateContext<IExamScoring>,
    payload: { score: ICaseScoreModel; showLoading: boolean }
  ) {
    if (payload.showLoading) {
      this.globalDialogService.showLoading();
    }
    const score = payload.score;
    return this.caseScoresService
      .updateCaseScore(score.examScoringId, score)
      .pipe(
        tap(() => {
          ctx.patchState({
            errors: null,
            examErrors: null,
          });
          if (payload.showLoading) {
            this.globalDialogService.showSuccessError(
              'Success',
              'Score Successfully Updated',
              true
            );
          }
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({ errors });
          if (payload.showLoading) {
            this.globalDialogService.showSuccessError(
              'Error',
              'Score update failed',
              false
            );
          }

          return of(errors);
        })
      );
  }

  @Action(DeleteCaseScore)
  deleteCaseScore(ctx: StateContext<IExamScoring>, payload: { id: number }) {
    const examScoringId = payload.id;
    return this.caseScoresService.deleteCaseScore(examScoringId).pipe(
      tap((result: ICaseScoreModel) => {
        // figure out how to update the store here
        ctx.patchState({
          // selectedCaseComment: result,
          errors: null,
          examErrors: null,
        });
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({ errors });
        return of(errors);
      })
    );
  }

  @Action(GetSelectedExamScores)
  getSelectedExamScores(
    ctx: StateContext<IExamScoring>,
    payload: { id: number }
  ) {
    // const state = ctx.getState();
    this.globalDialogService.showLoading();
    const examId = payload.id;
    return this.caseScoresService
      .retrieveCaseScoreReadOnly_GetByExamScheduleId(examId)
      .pipe(
        tap((result: ICaseScoreReadOnlyModel[]) => {
          ctx.patchState({
            selectedExamScores: result,
            errors: null,
            examErrors: null,
          });
          this.globalDialogService.closeOpenDialog();
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({ errors });
          this.globalDialogService.closeOpenDialog();
          return of(errors);
        })
      );
  }

  @Action(CreateExamScore)
  createExamScore(
    ctx: StateContext<IExamScoring>,
    payload: { model: IExamScoreModel; navigate: boolean }
  ) {
    this.globalDialogService.showLoading();
    return this.examScoreService.createExamScore(payload.model).pipe(
      tap(async (result: IExamScoreModel) => {
        ctx.patchState({
          selectedExamScores: undefined,
          errors: null,
          examErrors: null,
        });
        this.globalDialogService.showSuccessError(
          'Success',
          'Exam Submitted Successfully',
          true
        );
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({ examErrors: errors });
        this.globalDialogService.closeOpenDialog();
        return of(errors);
      })
    );
  }

  @Action(GetRoster)
  getRoster(
    ctx: StateContext<IExamScoring>,
    payload: { examinerUserId: number; examDate: string }
  ) {
    this.examDate = payload.examDate;
    this.globalDialogService.showLoading();
    return this.dashboardService
      .retrieveDashboardRosterReadOnly_GetByUserId(payload.examDate)
      .pipe(
        tap((dashboardRoster: IDashboardRosterReadOnlyModel[]) => {
          ctx.patchState({
            dashboardRoster: dashboardRoster,
            errors: null,
            examErrors: null,
          });
          this.globalDialogService.closeOpenDialog();
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({ errors });
          this.globalDialogService.closeOpenDialog();
          return of(errors);
        })
      );
  }

  @Action(SkipExam)
  skipExam(
    ctx: StateContext<IExamScoring>,
    payload: { examScheduleId: number; examDate: string; showLoading: boolean }
  ) {
    if (payload.showLoading) {
      this.globalDialogService.showLoading();
    }

    return this.examSessionsService
      .skipExamSessionReadOnly_SkipByExamScheduleId(payload.examScheduleId)
      .pipe(
        tap(() => {
          ctx.patchState({
            selectedExamScores: undefined,
            examErrors: null,
            errors: null,
          });
          ctx.dispatch(new GetExamineeList(payload.examDate));
          if (payload.showLoading) {
            this.globalDialogService.closeOpenDialog();
          }
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({ errors });
          this.globalDialogService.showSuccessError(
            'Error',
            'Exam Skip Failed',
            false
          );
          return of(errors);
        })
      );
  }

  @Action(ResetCaseCommentsData)
  resetCaseCommentsData(ctx: StateContext<IExamScoring>) {
    return this.examScoreService.resetCaseCommentsData().pipe(
      tap(() => {
        this.globalDialogService.showSuccessError(
          'Success',
          'Case Comments Reset Successfully',
          true
        );
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({ errors });
        this.globalDialogService.showSuccessError(
          'Error',
          'Case Comments Reset Failed',
          false
        );
        return of(errors);
      })
    );
  }

  @Action(ResetExamScoringData)
  resetExamScoringData(ctx: StateContext<IExamScoring>) {
    return this.examScoreService.resetExamScoring().pipe(
      tap(() => {
        this.globalDialogService.showSuccessError(
          'Success',
          'Exam Data Reset Successfully',
          true
        );
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({ errors });
        this.globalDialogService.showSuccessError(
          'Error',
          'Exam Data Reset Failed',
          false
        );
        return of(errors);
      })
    );
  }

  @Action(CreateCaseFeedback)
  createCaseFeedback(
    ctx: StateContext<IExamScoring>,
    payload: { model: ICaseFeedbackModel }
  ) {
    this.globalDialogService.showLoading();
    return this.caseFeedbackService.createCaseFeedback(payload.model).pipe(
      tap(async (result: ICaseFeedbackModel) => {
        ctx.patchState({
          selectedCaseFeedback: result,
          errors: null,
          examErrors: null,
        });
        await this.globalDialogService.showSuccessError(
          'Success',
          'Case Feedback Submitted Successfully',
          true
        );
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({ errors });
        this.globalDialogService.showSuccessError(
          'Error',
          'Case Feedback Submission Failed',
          false
        );
        return of(errors);
      })
    );
  }

  @Action(GetCaseFeedback)
  getCaseFeedback(ctx: StateContext<IExamScoring>, payload: { id: number }) {
    this.globalDialogService.showLoading();
    return this.caseFeedbackService
      .retrieveCaseFeedbackReadOnly_GetByExaminerId(payload.id)
      .pipe(
        tap((result: ICaseFeedbackReadOnlyModel) => {
          ctx.patchState({
            selectedCaseFeedback: result,
            errors: null,
            examErrors: null,
          });
          this.globalDialogService.closeOpenDialog();
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          if (httpError.status === 404) {
            ctx.patchState({
              selectedCaseFeedback: undefined,
            });
          }
          ctx.patchState({ errors });
          this.globalDialogService.closeOpenDialog();
          return of(errors);
        })
      );
  }

  @Action(UpdateCaseFeedback)
  updateCaseFeedback(
    ctx: StateContext<IExamScoring>,
    payload: { model: ICaseFeedbackModel }
  ) {
    this.globalDialogService.showLoading();
    return this.caseFeedbackService
      .updateCaseFeedback(payload.model.id, payload.model)
      .pipe(
        tap(async (result: ICaseFeedbackModel) => {
          ctx.patchState({
            selectedCaseFeedback: result,
            errors: null,
            examErrors: null,
          });
          await this.globalDialogService.showSuccessError(
            'Success',
            'Case Feedback Updated Successfully',
            true
          );
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({ errors });
          this.globalDialogService.showSuccessError(
            'Error',
            'Case Feedback Update Failed',
            false
          );
          return of(errors);
        })
      );
  }

  @Action(DeleteCaseFeedback)
  deleteCaseFeedback(ctx: StateContext<IExamScoring>, payload: { id: number }) {
    this.globalDialogService.showLoading();
    return this.caseFeedbackService.deleteCaseFeedback(payload.id).pipe(
      tap(async (result: ICaseFeedbackModel) => {
        ctx.patchState({
          selectedCaseFeedback: result,
          errors: null,
          examErrors: null,
        });
        await this.globalDialogService.showSuccessError(
          'Success',
          'Case Feedback Deleted Successfully',
          true
        );
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({ errors });
        this.globalDialogService.showSuccessError(
          'Error',
          'Case Feedback Delete Failed',
          false
        );
        return of(errors);
      })
    );
  }

  @Action(GetCaseDetailsAndFeedback)
  getCaseDetailsAndFeedback(
    ctx: StateContext<IExamScoring>,
    payload: { id: number }
  ): Observable<IExamScoring> {
    const joins = [
      this.getCaseContents(ctx, payload).pipe(catchError((error) => of(error))),
      this.getCaseFeedback(ctx, payload).pipe(catchError((error) => of(error))),
    ];

    return forkJoin(joins).pipe(
      map((examScoring: IExamScoring[]) => {
        return of(ctx.getState());
      }),
      share(),
      catchError((error) => {
        console.error('------- In Exam Scoring Store', error);
        return of(error);
      })
    );
  }

  @Action(ClearExamScoringErrors)
  clearGraduateMedicalEducationErrors(ctx: StateContext<IExamScoring>) {
    ctx.patchState({ errors: null, examErrors: null });
  }

  @Action(GetExaminerAgenda)
  getExaminerAgenda(
    ctx: StateContext<IExamScoring>,
    payload: { examHeaderId: number }
  ): Observable<IExamScoring> {
    return this.agendaService
      .retrieveAgendaReadOnly_GetByExamHeaderId(payload.examHeaderId)
      .pipe(
        tap((examinerAgenda: IAgendaReadOnlyModel) => {
          ctx.patchState({
            examinerAgenda,
            errors: null,
            examErrors: null,
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({ errors });
          return of(errors);
        })
      );
  }

  @Action(GetExaminerConflict)
  getExaminerConflict(
    ctx: StateContext<IExamScoring>,
    payload: { examHeaderId: number }
  ): Observable<IExamScoring> {
    return this.conflictSerive
      .retrieveConflictReadOnly_GetByExamHeaderId(payload.examHeaderId)
      .pipe(
        tap((examinerConflict: IConflictReadOnlyModel) => {
          ctx.patchState({
            examinerConflict,
            errors: null,
            examErrors: null,
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({ errors });
          return of(errors);
        })
      );
  }

  @Action(GetExamHeaderId)
  getExamHeaderId(
    ctx: StateContext<IExamScoring>,
    payload: { isCeScoreTesting: boolean }
  ) {
    const state = ctx.getState();
    if (payload.isCeScoreTesting) {
      ctx.patchState({
        examHeaderId: 494,
      });
    }

    return of(state.examHeaderId);
  }
}
