import { Injectable } from '@angular/core';
import { catchError, tap } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { Action, State, StateContext, StateToken } from '@ngxs/store';
import { IFormErrors } from '../../shared/common';
import { GetExamHistory } from './exam-history.actions';
import { ExaminationsService } from 'src/app/api/services/examinations/examinations.service';
import { IExamHistoryReadOnlyModel } from 'src/app/api/models/examinations/exam-history-read-only.model';

export interface IExamHistory {
  examHistory: IExamHistoryReadOnlyModel[];
  errors?: IFormErrors | null;
}

export const EXAM_HISTORY_STATE_TOKEN = new StateToken<IExamHistory>(
  'examHistory'
);

@State<IExamHistory>({
  name: EXAM_HISTORY_STATE_TOKEN,
  defaults: {
    examHistory: [],
    errors: null,
  },
})
@Injectable()
export class ExamHistoryState {
  constructor(private examinationService: ExaminationsService) {}

  @Action(GetExamHistory)
  getExamHistory(
    ctx: StateContext<IExamHistory>
  ): Observable<IExamHistoryReadOnlyModel[] | undefined> {
    // if (ctx.getState()?.examHistory) {
    //   return of(ctx.getState()?.examHistory);
    // }

    return this.examinationService
      .retrieveExamHistoryReadOnly_GetByUserId()
      .pipe(
        tap((examHistory: IExamHistoryReadOnlyModel[]) => {
          examHistory.forEach((exam) => {
            if (exam.result === 'P') exam.result = 'Pass';
            else if (exam.result === 'F') exam.result = 'Fail';
          });
          ctx.patchState({
            examHistory,
          });
        }),
        catchError((error) => {
          console.error('------- In Medical Training Store', error);
          console.error(error);
          return of(error);
        })
      );
  }
}
