import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { Action, State, StateContext, StateToken } from '@ngxs/store';
import { IFormErrors } from '../../shared/common';
import { GetExamDirectory } from './exam-process.actions';
import { IExamOverviewReadOnlyModel } from 'src/app/api/models/examinations/exam-overview-read-only.model';
import { ExaminationsService } from 'src/app/api/services/examinations/examinations.service';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';

export interface IExamProcess {
  examDirectory: IExamOverviewReadOnlyModel[];
  errors?: IFormErrors | null;
}

export const EXAM_PROCESS_STATE_TOKEN = new StateToken<IExamProcess>(
  'examProcess'
);

@State<IExamProcess>({
  name: EXAM_PROCESS_STATE_TOKEN,
  defaults: {
    examDirectory: [],
    errors: null,
  },
})
@Injectable()
export class ExamProcessState {
  constructor(
    private examinationsService: ExaminationsService,
    private globalDialogService: GlobalDialogService
  ) {}

  @Action(GetExamDirectory)
  getExamDirectory(
    ctx: StateContext<IExamProcess>
  ): Observable<IExamOverviewReadOnlyModel[]> {
    this.globalDialogService.showLoading();
    if (ctx.getState().examDirectory.length > 0) {
      this.globalDialogService.closeOpenDialog();
      return of(ctx.getState().examDirectory);
    }
    return this.examinationsService.retrieveExamOverviewReadOnly_GetAll().pipe(
      tap((examDirectory) => {
        ctx.patchState({
          examDirectory,
        });
        this.globalDialogService.closeOpenDialog();
      }),
      catchError((error) => {
        console.error('------- In Exam Process', error);
        console.error(error);
        this.globalDialogService.closeOpenDialog();
        return of(error);
      })
    );
  }
}
