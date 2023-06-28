import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { catchError, mergeMap, tap } from 'rxjs/operators';
import { of } from 'rxjs';
import { Action, State, StateContext, StateToken } from '@ngxs/store';
import { CasesService } from '../../api';
import { IFormErrors } from '../../shared/common';
import {
  GetExamRosters,
  GetExamCases,
  GetExamScores,
  GetCaseContents,
  GetCaseComments,
  ClearExamScoringErrors,
} from './exam-scoring.actions';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';

export interface IExamScoring {
  rosters: any[] | undefined;
  cases: any[] | undefined;
  scores: any[] | undefined;
  selectedCase: any | undefined;
  caseComments: any[] | undefined;
  errors: IFormErrors | null;
}

export const EXAM_SCORING_STATE_TOKEN = new StateToken<IExamScoring>(
  'examScoring'
);

@State<IExamScoring>({
  name: EXAM_SCORING_STATE_TOKEN,
  defaults: {
    rosters: undefined,
    cases: undefined,
    scores: undefined,
    selectedCase: undefined,
    caseComments: undefined,
    errors: null,
  },
})
@Injectable()
export class ExamScoringState {
  constructor(private casesService: CasesService) {}

  @Action(GetExamCases)
  getExamCases(
    ctx: StateContext<IExamScoring>,
    payload: { id1: number; id2: number }
  ) {
    // const state = ctx.getState();
    const sessionId1 = payload.id1;
    const sessionId2 = payload.id2;
    return this.casesService
      .retrieveCaseRosterReadOnly_GetByScheduleId(sessionId1, sessionId2)
      .pipe(
        tap((result: any) => {
          ctx.patchState({
            cases: result,
            errors: null,
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({ errors });
          return of(errors);
        })
      );
  }

  @Action(ClearExamScoringErrors)
  clearGraduateMedicalEducationErrors(ctx: StateContext<IExamScoring>) {
    ctx.patchState({ errors: null });
  }
}
