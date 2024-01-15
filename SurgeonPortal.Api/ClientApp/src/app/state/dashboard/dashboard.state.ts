import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Action, State, StateContext, StateToken } from '@ngxs/store';
import { IProgramReadOnlyModel } from 'src/app/api/models/trainees/program-read-only.model';
import { ICertificationReadOnlyModel } from 'src/app/api/models/surgeons/certification-read-only.model';
import { ProgramsService } from 'src/app/api/services/trainees/programs.service';
import { CertificationsService } from 'src/app/api/services/surgeons/certifications.service';
import {
  GetAlertsAndNotices,
  GetDashboardCertificationInformation,
  GetDashboardCertificationStatus,
  GetDashboardProgramInformation,
  GetMeetingRequitments,
  GetTraineeRegistrationStatus,
} from './dashboard.actions';
import { catchError, of, tap } from 'rxjs';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';
import { ExamService } from 'src/app/api/services/trainees/exam.service';
import { IRegistrationStatusReadOnlyModel } from 'src/app/api/models/trainees/registration-status-read-only.model';
import { IQualifyingExamReadOnlyModel } from 'src/app/api/models/examinations/qualifying-exam-read-only.model';
import { QualifyingExamService } from 'src/app/api/services/examinations/qualifying-exam.service';
import { ICertificationStatusReadOnlyModel } from 'src/app/api/models/user/certification-status-read-only.model';
import { CertificationStatusService } from 'src/app/api/services/user/certification-status.service';
import { IRequirementsReadOnlyModel } from 'src/app/api/models/continuouscertification/requirements-read-only.model';
import { RequirementsService } from 'src/app/api/services/continuouscertification/requirements.service';

export interface ICertification extends ICertificationReadOnlyModel {
  status?: string;
}

export interface ICertificationStatus
  extends ICertificationStatusReadOnlyModel {
  lapsedPath?: boolean;
}

export interface IDashboardState {
  certificates: ICertificationReadOnlyModel[];
  certificationStatus: ICertificationStatus | null;
  registrationStatus: IRegistrationStatusReadOnlyModel | null;
  alertsAndNotices: IQualifyingExamReadOnlyModel | undefined;
  programs: IProgramReadOnlyModel;
  meetingRequirements: IRequirementsReadOnlyModel | undefined;
}

const USER_ACCOUNT_STATE_TOKEN = new StateToken<IDashboardState>('dashboard');

@State({
  name: USER_ACCOUNT_STATE_TOKEN,
  defaults: {
    certificates: [],
    certificationStatus: null,
    registrationStatus: null,
    alertsAndNotices: undefined,
    meetingRequirements: undefined,
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
    private examService: ExamService,
    private globalDialogService: GlobalDialogService,
    private qualifyingExamService: QualifyingExamService,
    private certificationStatusService: CertificationStatusService,
    private requirementsService: RequirementsService
  ) {}
  //user
  @Action(GetDashboardCertificationStatus) getDashboardCertificationStatus(
    ctx: StateContext<IDashboardState>
  ) {
    const state = ctx.getState();
    if (state.certificationStatus !== null) {
      return state.certificationStatus;
    }

    return this.certificationStatusService
      .retrieveCertificationStatusReadOnly_GetByUserId()
      .pipe(
        tap((result: ICertificationStatusReadOnlyModel) => {
          const res = result as ICertificationStatus;
          res.lapsedPath = res.certificationStatusId === 8; // set lapsed path based on status ID
          ctx.patchState({
            certificationStatus: res,
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          return of(errors);
        })
      );
  }

  // trainee
  @Action(GetDashboardProgramInformation) getDashboardProgramInformation(
    ctx: StateContext<IDashboardState>
  ) {
    const state = ctx.getState();
    return this.programsService.retrieveProgramReadOnly_GetByUserId().pipe(
      tap((result: IProgramReadOnlyModel) => {
        const res = result as IProgramReadOnlyModel;
        ctx.patchState({
          programs: res,
        });
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        return of(errors);
      })
    );
  }

  // surgeon
  @Action(GetDashboardCertificationInformation)
  getDashboardCertificationInformation(ctx: StateContext<IDashboardState>) {
    const state = ctx.getState();
    if (state.certificates.length > 0) {
      return state.certificates;
    }
    this.globalDialogService.showLoading();
    return this.certificationsService
      .retrieveCertificationReadOnly_GetByUserId()
      .pipe(
        tap((result: ICertificationReadOnlyModel[]) => {
          const res = result as ICertification[];
          res.forEach((cert) => {
            if (
              cert.isClinicallyInactive !== null &&
              cert.isClinicallyInactive !== undefined
            ) {
              cert.status = cert.isClinicallyInactive
                ? 'Clinically Inactive'
                : 'Active';
            }
          });
          ctx.patchState({
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

  // trainee
  @Action(GetTraineeRegistrationStatus)
  getTraineeRegistrationStatus(
    ctx: StateContext<IDashboardState>,
    payload: GetTraineeRegistrationStatus
  ) {
    const state = ctx.getState();
    return this.examService
      .retrieveRegistrationStatusReadOnly_GetByExamCode(payload.examCode)
      .pipe(
        tap((result: IRegistrationStatusReadOnlyModel) => {
          ctx.patchState({
            registrationStatus: result,
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

  // trainee
  @Action(GetAlertsAndNotices)
  getAlertsAndNotices(ctx: StateContext<IDashboardState>) {
    const state = ctx.getState();
    this.globalDialogService.showLoading();
    return this.qualifyingExamService.retrieveQualifyingExamReadOnly_Get().pipe(
      tap((result: IQualifyingExamReadOnlyModel) => {
        const alertsAndNotices = result as IQualifyingExamReadOnlyModel;
        ctx.patchState({
          alertsAndNotices: alertsAndNotices,
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

  @Action(GetMeetingRequitments)
  getMeetingRequitments(
    ctx: StateContext<IDashboardState>,
    payload: GetMeetingRequitments
  ) {
    return this.requirementsService
      .retrieveRequirementsReadOnly_GetByUserId(payload.userId)
      .pipe(
        tap((result: IRequirementsReadOnlyModel) => {
          ctx.patchState({
            meetingRequirements: result,
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          return of(errors);
        })
      );
  }
}
