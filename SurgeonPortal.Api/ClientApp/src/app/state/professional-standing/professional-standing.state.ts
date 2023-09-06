import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { of } from 'rxjs';
import { Action, State, StateContext, StateToken } from '@ngxs/store';
import {
  IMedicalLicenseReadOnlyModel,
  IMedicalLicenseModel,
  IUserProfessionalStandingModel,
  MedicalLicenseService,
  UserProfessionalStandingService,
  UserAppointmentService,
  IUserAppointmentModel,
  SanctionsService,
  ISanctionsModel,
} from '../../api';
import { IFormErrors } from '../../shared/common';
import {
  GetPSMedicalLicenseList,
  GetPSMedicalLicenseDetails,
  CreatePSMedicalLicense,
  UpdatePSMedicalLicense,
  DeletePSMedicalLicense,
  GetPSAppointmentsAndPrivilegesList,
  GetPSAppointmentAndPrivilegeDetails,
  CreatePSAppointmentAndPrivilege,
  UpdatePSAppointmentAndPrivilege,
  DeletePSAppointmentAndPrivilege,
  GetUserProfessionalStandingDetails,
  CreateUserProfessionalStandingDetails,
  UpdateUserProfessionalStandingDetails,
  ClearProfessionalStandingErrors,
  GetProfessionalStandingSanctionsDetails,
  CreateProfessionalStandingSanctionsDetails,
  UpdateProfessionalStandingSanctionsDetails,
} from './professional-standing.actions';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';

export interface IProfessionalStanding {
  medicalLiscenseList: IMedicalLicenseReadOnlyModel[];
  selectedMedicalLicense: IMedicalLicenseModel | undefined;
  userProfessionalStandingDetails: IUserProfessionalStandingModel | undefined;
  sanctions: ISanctionsModel | undefined;
  allAppointments: any[];
  selectedAppointment: any;
  claims: string[];
  medicalLicenseErrors?: IFormErrors | null;
  appointmentErrors?: IFormErrors | null;
  professionalStandingErrors?: IFormErrors | null;
  sanctionsErrors?: IFormErrors | null;
}

export const PROFESSIONAL_STANDING_STATE_TOKEN =
  new StateToken<IProfessionalStanding>('professionalStanding');

@State<IProfessionalStanding>({
  name: PROFESSIONAL_STANDING_STATE_TOKEN,
  defaults: {
    medicalLiscenseList: [],
    selectedMedicalLicense: undefined,
    userProfessionalStandingDetails: undefined,
    sanctions: undefined,
    allAppointments: [],
    selectedAppointment: undefined,
    claims: [],
    medicalLicenseErrors: null,
    appointmentErrors: null,
    professionalStandingErrors: null,
    sanctionsErrors: null,
  },
})
@Injectable()
export class ProfessionalStandingState {
  constructor(
    private medicalLicenseService: MedicalLicenseService,
    private userProfessionalStandingService: UserProfessionalStandingService,
    private userAppointmentService: UserAppointmentService,
    private sanctionsService: SanctionsService,
    private globalDialogService: GlobalDialogService
  ) {}

  @Action(GetPSMedicalLicenseList)
  getMedicalLicenseList(ctx: StateContext<IProfessionalStanding>) {
    // const state = ctx.getState();
    return this.medicalLicenseService
      .retrieveMedicalLicenseReadOnly_GetByUserId()
      .pipe(
        tap((response) => {
          ctx.patchState({
            medicalLiscenseList: response,
            medicalLicenseErrors: null,
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({ medicalLicenseErrors: errors });
          return of(errors);
        })
      );
  }

  @Action(GetPSMedicalLicenseDetails)
  getMedicalLicenseDetails(
    ctx: StateContext<IProfessionalStanding>,
    payload: { id: number }
  ) {
    // const state = ctx.getState();
    const licenseId = payload.id;
    return this.medicalLicenseService
      .retrieveMedicalLicense_GetById(licenseId)
      .pipe(
        tap((response) => {
          ctx.patchState({
            selectedMedicalLicense: response,
            medicalLicenseErrors: null,
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({ medicalLicenseErrors: errors });
          return of(errors);
        })
      );
  }

  @Action(CreatePSMedicalLicense)
  createMedicalLicense(
    ctx: StateContext<IProfessionalStanding>,
    payload: { license: IMedicalLicenseModel }
  ) {
    const state = ctx.getState();
    const medicalLicense = payload.license;
    return this.medicalLicenseService.createMedicalLicense(medicalLicense).pipe(
      tap((response) => {
        const readOnlyResult = {
          licenseId: response.licenseId,
          userId: response.userId,
          issuingStateId: response.issuingStateId,
          issuingState: response.issuingState,
          licenseNumber: response.licenseNumber,
          licenseTypeId: response.licenseTypeId,
          licenseType: response.licenseType,
          issueDate: response.issueDate,
          expireDate: response.expireDate,
          reportingOrganization: response.reportingOrganization,
        };
        ctx.patchState({
          medicalLiscenseList: [readOnlyResult, ...state.medicalLiscenseList],
          selectedMedicalLicense: undefined,
          medicalLicenseErrors: null,
        });
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({ medicalLicenseErrors: errors });
        return of(errors);
      })
    );
  }

  @Action(UpdatePSMedicalLicense)
  updateMedicalLicense(
    ctx: StateContext<IProfessionalStanding>,
    payload: { license: IMedicalLicenseModel }
  ) {
    const state = ctx.getState();
    const medicalLicense = payload.license;
    return this.medicalLicenseService
      .updateMedicalLicense(medicalLicense.licenseId, medicalLicense)
      .pipe(
        tap((response) => {
          const readOnlyResult = {
            licenseId: response.licenseId,
            userId: response.userId,
            issuingStateId: response.issuingStateId,
            issuingState: response.issuingState,
            licenseNumber: response.licenseNumber,
            licenseTypeId: response.licenseTypeId,
            licenseType: response.licenseType,
            issueDate: response.issueDate,
            expireDate: response.expireDate,
            reportingOrganization: response.reportingOrganization,
          };
          const updatedMedicalLicenseList = state.medicalLiscenseList.map(
            (item) =>
              item.licenseId === readOnlyResult.licenseId
                ? readOnlyResult
                : item
          );
          ctx.patchState({
            medicalLiscenseList: updatedMedicalLicenseList,
            selectedMedicalLicense: undefined,
            medicalLicenseErrors: null,
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({ medicalLicenseErrors: errors });
          return of(errors);
        })
      );
  }

  @Action(DeletePSMedicalLicense)
  deleteMedicalLicense(
    ctx: StateContext<IProfessionalStanding>,
    { id }: DeletePSMedicalLicense
  ) {
    const state = ctx.getState();
    return this.medicalLicenseService.deleteMedicalLicense(id).pipe(
      tap(() => {
        const updatedMedicalLicenseList = state.medicalLiscenseList.filter(
          (item) => item.licenseId !== id
        );
        ctx.patchState({
          medicalLiscenseList: updatedMedicalLicenseList,
          selectedMedicalLicense: undefined,
          medicalLicenseErrors: null,
        });
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({ medicalLicenseErrors: errors });
        return of(errors);
      })
    );
  }

  @Action(GetPSAppointmentsAndPrivilegesList)
  getPSAppointmentsAndPrivilegesList(ctx: StateContext<IProfessionalStanding>) {
    // const state = ctx.getState();
    return this.userAppointmentService
      .retrieveUserAppointmentReadOnly_GetByUserId()
      .pipe(
        tap((response) => {
          ctx.patchState({
            allAppointments: response,
            appointmentErrors: null,
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({ appointmentErrors: errors });
          return of(errors);
        })
      );
  }

  @Action(GetPSAppointmentAndPrivilegeDetails)
  getPSAppointmentAndPrivilegeDetails(
    ctx: StateContext<IProfessionalStanding>,
    payload: { id: number }
  ) {
    // const state = ctx.getState();
    const appointmentId = payload.id;
    return this.userAppointmentService
      .retrieveUserAppointment_GetById(appointmentId)
      .pipe(
        tap((response) => {
          ctx.patchState({
            selectedAppointment: response,
            appointmentErrors: null,
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({ appointmentErrors: errors });
          return of(errors);
        })
      );
  }

  @Action(CreatePSAppointmentAndPrivilege)
  createPSAppointmentAndPrivilege(
    ctx: StateContext<IProfessionalStanding>,
    payload: { data: IUserAppointmentModel }
  ) {
    const state = ctx.getState();
    const appointment = payload.data;
    return this.userAppointmentService.createUserAppointment(appointment).pipe(
      tap((response) => {
        const readOnlyResult = {
          apptId: response.apptId,
          userId: response.userId,
          practiceTypeId: response.practiceTypeId,
          practiceType: response.practiceType,
          appointmentTypeId: response.appointmentTypeId,
          appointmentType: response.appointmentType,
          organizationTypeId: response.organizationTypeId,
          authorizingOfficial: response.authorizingOfficial,
          organizationType: response.organizationType,
          organizationId: response.organizationId,
          stateCode: response.stateCode,
          other: response.other,
          organizationName: response.organizationName,
        };
        ctx.patchState({
          allAppointments: [readOnlyResult, ...state.allAppointments],
          selectedAppointment: undefined,
          appointmentErrors: null,
        });
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({ appointmentErrors: errors });
        return of(errors);
      })
    );
  }

  @Action(UpdatePSAppointmentAndPrivilege)
  updatePSAppointmentAndPrivilege(
    ctx: StateContext<IProfessionalStanding>,
    payload: { data: IUserAppointmentModel }
  ) {
    const state = ctx.getState();
    const appointment = payload.data;
    return this.userAppointmentService
      .updateUserAppointment(appointment.apptId, appointment)
      .pipe(
        tap((response) => {
          const readOnlyResult = {
            apptId: response.apptId,
            userId: response.userId,
            practiceTypeId: response.practiceTypeId,
            practiceType: response.practiceType,
            appointmentTypeId: response.appointmentTypeId,
            appointmentType: response.appointmentType,
            organizationTypeId: response.organizationTypeId,
            authorizingOfficial: response.authorizingOfficial,
            organizationType: response.organizationType,
            organizationId: response.organizationId,
            stateCode: response.stateCode,
            other: response.other,
            organizationName: response.organizationName,
          };
          const updatedAppointmentsList = state.allAppointments.map((item) =>
            item.apptId === readOnlyResult.apptId ? readOnlyResult : item
          );
          ctx.patchState({
            allAppointments: updatedAppointmentsList,
            selectedAppointment: undefined,
            appointmentErrors: null,
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({ appointmentErrors: errors });
          return of(errors);
        })
      );
  }

  @Action(DeletePSAppointmentAndPrivilege)
  deletePSAppointmentAndPrivilege(
    ctx: StateContext<IProfessionalStanding>,
    { id }: DeletePSAppointmentAndPrivilege
  ) {
    const state = ctx.getState();
    return this.userAppointmentService.deleteUserAppointment(id).pipe(
      tap(() => {
        const updatedAppointmentsList = state.allAppointments.filter(
          (item) => item.apptId !== id
        );
        ctx.patchState({
          allAppointments: updatedAppointmentsList,
          selectedAppointment: undefined,
          appointmentErrors: null,
        });
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({ appointmentErrors: errors });
        return of(errors);
      })
    );
  }

  @Action(GetUserProfessionalStandingDetails)
  getUserProfessionalStandingDetails(ctx: StateContext<IProfessionalStanding>) {
    //const state = ctx.getState();
    return this.userProfessionalStandingService
      .retrieveUserProfessionalStanding_GetByUserId()
      .pipe(
        tap((response) => {
          ctx.patchState({
            userProfessionalStandingDetails: response,
            professionalStandingErrors: null,
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({ professionalStandingErrors: errors });
          return of(errors);
        })
      );
  }

  @Action(CreateUserProfessionalStandingDetails)
  createUserProfessionalStandingDetails(
    ctx: StateContext<IProfessionalStanding>,
    payload: { details: IUserProfessionalStandingModel }
  ) {
    // const state = ctx.getState();
    const details = payload.details;
    return this.userProfessionalStandingService
      .createUserProfessionalStanding(details)
      .pipe(
        tap((response) => {
          ctx.patchState({
            userProfessionalStandingDetails: response,
            professionalStandingErrors: null,
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({ professionalStandingErrors: errors });
          return of(errors);
        })
      );
  }

  @Action(UpdateUserProfessionalStandingDetails)
  updateUserProfessionalStandingDetails(
    ctx: StateContext<IProfessionalStanding>,
    payload: { details: IUserProfessionalStandingModel }
  ) {
    // const state = ctx.getState();
    this.globalDialogService.showLoading();
    const details = payload.details;
    return this.userProfessionalStandingService
      .updateUserProfessionalStanding(details)
      .pipe(
        tap((response) => {
          ctx.patchState({
            userProfessionalStandingDetails: response,
            professionalStandingErrors: null,
          });
          this.globalDialogService.showSuccessError(
            'Success',
            'Professional Standing Details Updated Successfully',
            true
          );
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({ professionalStandingErrors: errors });
          return of(errors);
        })
      );
  }

  @Action(GetProfessionalStandingSanctionsDetails)
  getProfessionalStandingSanctionsDetails(
    ctx: StateContext<IProfessionalStanding>
  ) {
    //const state = ctx.getState();
    return this.sanctionsService.retrieveSanctions_GetByUserId().pipe(
      tap((response) => {
        ctx.patchState({
          sanctions: response,
          sanctionsErrors: null,
        });
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({ sanctionsErrors: errors });
        return of(errors);
      })
    );
  }

  @Action(CreateProfessionalStandingSanctionsDetails)
  createProfessionalStandingSanctionsDetails(
    ctx: StateContext<IProfessionalStanding>,
    payload: { data: ISanctionsModel }
  ) {
    const sanctions = payload.data;
    return this.sanctionsService.createSanctions(sanctions).pipe(
      tap((response) => {
        ctx.patchState({
          sanctions: response,
          sanctionsErrors: null,
        });
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({ sanctionsErrors: errors });
        return of(errors);
      })
    );
  }

  @Action(UpdateProfessionalStandingSanctionsDetails)
  updateProfessionalStandingSanctionsDetails(
    ctx: StateContext<IProfessionalStanding>,
    payload: { data: ISanctionsModel }
  ) {
    // const state = ctx.getState();
    const sanctions = payload.data;
    return this.sanctionsService.updateSanctions(sanctions).pipe(
      tap((response) => {
        ctx.patchState({
          sanctions: response,
          sanctionsErrors: null,
        });
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({ sanctionsErrors: errors });
        return of(errors);
      })
    );
  }

  @Action(ClearProfessionalStandingErrors)
  clearProfessionalStandingErrors(ctx: StateContext<IProfessionalStanding>) {
    ctx.patchState({
      medicalLicenseErrors: null,
      appointmentErrors: null,
      professionalStandingErrors: null,
      sanctionsErrors: null,
    });
  }
}
