import { Injectable } from '@angular/core';
import { catchError, tap } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { Action, State, StateContext, StateToken } from '@ngxs/store';
import { IFormErrors } from '../../shared/common';
import {
  ExamFeeTransaction,
  GetApplicationFee,
  GetExamDirectory,
  GetExamFeeByExamId,
  GetExamFees,
} from './exam-process.actions';
import { IExamOverviewReadOnlyModel } from 'src/app/api/models/examinations/exam-overview-read-only.model';
import { ExaminationsService } from 'src/app/api/services/examinations/examinations.service';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';
import { IExamFeeReadOnlyModel } from 'src/app/api/models/billing/exam-fee-read-only.model';
import { ExamFeeService } from 'src/app/api/services/billing/exam-fee.service';
import { IExamFeeTransactionModel } from 'src/app/api/models/billing/exam-fee-transaction.mode';
import { ExamFeeTransactionService } from 'src/app/api/services/billing/exam-fee-transaction.service';
import { environment } from 'src/environments/environment';
import { IApplicationFeeReadOnlyModel } from 'src/app/api/models/billing/application-fee-read-only.model';
import { ApplicationFeeService } from 'src/app/api/services/billing/application-fee.service';

export interface IExamProcess {
  examDirectory: IExamOverviewReadOnlyModel[];
  examFees: IExamFeeReadOnlyModel[];
  examFeeByExamId?: IExamFeeReadOnlyModel[];
  examFeeTransaction: IExamFeeTransactionModel | undefined;
  applicationFee?: IApplicationFeeReadOnlyModel[];
  errors?: IFormErrors | null;
}

export const EXAM_PROCESS_STATE_TOKEN = new StateToken<IExamProcess>(
  'examProcess'
);

@State<IExamProcess>({
  name: EXAM_PROCESS_STATE_TOKEN,
  defaults: {
    examDirectory: [],
    examFees: [],
    examFeeByExamId: [],
    examFeeTransaction: undefined,
    applicationFee: [],
    errors: null,
  },
})
@Injectable()
export class ExamProcessState {
  constructor(
    private examinationsService: ExaminationsService,
    private examFeeService: ExamFeeService,
    private examFeeTransactionService: ExamFeeTransactionService,
    private globalDialogService: GlobalDialogService,
    private applicationFeeService: ApplicationFeeService
  ) {}

  @Action(GetExamDirectory)
  getExamDirectory(
    ctx: StateContext<IExamProcess>
  ): Observable<IExamOverviewReadOnlyModel[]> {
    this.globalDialogService.showLoading();
    const state = ctx.getState();
    if (state && state.examDirectory.length > 0) {
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

  @Action(GetExamFeeByExamId)
  getExamFeeByExamId(
    ctx: StateContext<IExamProcess>,
    payload: GetExamFeeByExamId
  ): Observable<IExamFeeReadOnlyModel[]> {
    this.globalDialogService.showLoading();
    return this.examFeeService
      .retrieveExamFeeReadOnly_GetByExamId(payload.examId)
      .pipe(
        tap((examFee: IExamFeeReadOnlyModel) => {
          const fee = [examFee];
          ctx.patchState({
            examFeeByExamId: fee,
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

  @Action(GetApplicationFee)
  getApplicationFee(
    ctx: StateContext<IExamProcess>,
    payload: GetApplicationFee
  ): Observable<IApplicationFeeReadOnlyModel[]> {
    this.globalDialogService.showLoading();
    return this.applicationFeeService
      .retrieveApplicationFeeReadOnly_GetByExamId(14, payload.examId)
      .pipe(
        tap((applicationFee: IApplicationFeeReadOnlyModel) => {
          const fee = [applicationFee];
          ctx.patchState({
            applicationFee: fee,
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

  @Action(GetExamFees)
  getExamFees(
    ctx: StateContext<IExamProcess>
  ): Observable<IExamFeeReadOnlyModel[]> {
    this.globalDialogService.showLoading();
    // const state = ctx.getState();
    // if (state && state.examFees?.length > 0) {
    //   this.globalDialogService.closeOpenDialog();
    //   return of(ctx.getState().examFees);
    // }
    return this.examFeeService.retrieveExamFeeReadOnly_GetByUserId().pipe(
      tap((examFees) => {
        ctx.patchState({
          examFees,
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

  @Action(ExamFeeTransaction)
  examFeeTransaction(
    ctx: StateContext<IExamProcess>,
    payload: ExamFeeTransaction
  ): Observable<IExamFeeTransactionModel> {
    return this.examFeeTransactionService
      .retrieveExamFeeTransactionToken(payload.model)
      .pipe(
        tap((examFeeTransaction) => {
          ctx.patchState({
            examFeeTransaction,
          });

          const transactionToken = examFeeTransaction.transactionToken;

          if (transactionToken) {
            window.open(
              `${environment.convergePayBaseUrl}/hosted-payments?ssl_txn_auth_token=${transactionToken}`,
              '_self'
            );
          }
        }),
        catchError((error) => {
          console.error('------- In Exam Process', error);
          console.error(error);
          this.globalDialogService.showSuccessError(
            error.status,
            'There was an error processing your request. Please try again later.',
            false
          );
          return of(error);
        })
      );
  }
}
