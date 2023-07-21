import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Action, State, StateContext, StateToken } from '@ngxs/store';
import { IProgramReadOnlyModel } from 'src/app/api/models/trainees/program-read-only.model';
import { ICertificationReadOnlyModel } from 'src/app/api/models/surgeons/certification-read-only.model';
import { ProgramsService } from 'src/app/api/services/trainees/programs.service';
import { CertificationsService } from 'src/app/api/services/surgeons/certifications.service';
import {
  GetDashboardCertificationInformation,
  GetDashboardProgramInformation,
} from './dashboard.actions';
import { catchError, of, tap } from 'rxjs';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';

export interface IDashboardState {
  certificates: ICertificationReadOnlyModel[];
  programs: IProgramReadOnlyModel;
}

const USER_ACCOUNT_STATE_TOKEN = new StateToken<IDashboardState>('dashboard');

@State({
  name: USER_ACCOUNT_STATE_TOKEN,
  defaults: {
    certificates: [],
    programs: {
      programName: '',
      programDirector: '',
      programNumber: '',
      exam: '',
      clinicalLevel: '',
      city: '',
      state: '',
    },
  },
})
@Injectable()
export class DashboardState {
  constructor(
    private programsService: ProgramsService,
    private certificationsService: CertificationsService,
    private globalDialogService: GlobalDialogService
  ) {}
  @Action(GetDashboardProgramInformation) getDashboardProgramInformation(
    ctx: StateContext<IDashboardState>
  ) {
    const state = ctx.getState();
    return this.programsService.retrieveProgramReadOnly_GetByUserId().pipe(
      tap((result: IProgramReadOnlyModel) => {
        const res = result as IProgramReadOnlyModel;
        ctx.setState({
          ...state,
          programs: res,
        });
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        return of(errors);
      })
    );
  }

  @Action(GetDashboardCertificationInformation)
  getDashboardCertificationInformation(ctx: StateContext<IDashboardState>) {
    const state = ctx.getState();
    this.globalDialogService.showLoading();
    return this.certificationsService
      .retrieveCertificationReadOnly_GetByUserId()
      .pipe(
        tap((result: ICertificationReadOnlyModel[]) => {
          const res = result as ICertificationReadOnlyModel[];
          ctx.setState({
            ...state,
            certificates: res,
          });
          this.globalDialogService.closeOpenDialog();
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          this.globalDialogService.closeOpenDialog();
          return of(errors);
        })
      );
  }
}
