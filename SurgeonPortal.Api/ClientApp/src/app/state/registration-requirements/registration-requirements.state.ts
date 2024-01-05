import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Action, State, StateContext, StateToken } from '@ngxs/store';
import { of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { IAccommodationModel } from 'src/app/api/models/examinations/accommodation.model';
import { IStatuses } from 'src/app/api/models/users/statuses.model';
import { AccommodationService } from 'src/app/api/services/examinations/accommodation.service';
import { IFormErrors } from 'src/app/shared/common';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';
import {
  ApplyForQeExam,
  CreateAccommodation,
  CreatePdReferenceLetter,
  GetAccommodations,
  GetPdReferenceLetter,
  GetQeAttestations,
  GetQeExamEligibility,
  GetRegistrationRequirementsTitle,
  GetResgistrationRequirmentsStatuses,
  UpdateAccommodation,
  UpdateQeAttestations,
} from './registration-requirements.actions';
import { IPdReferenceLetterModel } from 'src/app/api/models/examinations/pd-reference-letter.model';
import { PdReferenceLetterService } from 'src/app/api/services/examinations/pd-reference-letter.service';
import { IExamTitleReadOnlyModel } from 'src/app/api/models/examinations/exam-title-read-only.model';
import { ExaminationsService } from 'src/app/api/services/examinations/examinations.service';
import { IQeExamEligibilityReadOnlyModel } from 'src/app/api/models/examinations/qe-exam-eligibility-read-only.model';
import { QeExamEligibilityService } from 'src/app/api/services/examinations/qe-exam-eligibility.service';
import { IQeAttestationReadOnlyModel } from 'src/app/api/models/examinations/qe-attestation-read-only.model';
import { QeAttestationService } from 'src/app/api/services/examinations/qe-attestation.service';
import { IAttestationReadOnlyModel } from 'src/app/api/models/continuouscertification/attestation-read-only.model';
import { QeDashboardStatusService } from 'src/app/api/services/examinations/qe-dashboard-status.service';
import { IQeDashboardStatusReadOnlyModel } from 'src/app/api/models/examinations/qe-dashboard-status-read-only.model';
import { statusTypes } from '../continuous-certification/statusTypes';

export interface IExamEligibility extends IQeExamEligibilityReadOnlyModel {
  applicationIsOpen: boolean;
}

export interface IRegistrationRequirements {
  qeAttestations: IAttestationReadOnlyModel[] | undefined;
  registrationRequirementsStatuses?: IStatuses[];
  accommodation?: IAccommodationModel;
  pdReferenceLetter?: IPdReferenceLetterModel[];
  examTitle?: IExamTitleReadOnlyModel | undefined;
  qeExamEligibility: IExamEligibility[];
  errors?: IFormErrors | null;
}

export const REGREQ_STATE_TOKEN = new StateToken<IRegistrationRequirements>(
  'registration_requirements'
);

@State<IRegistrationRequirements>({
  name: REGREQ_STATE_TOKEN,
  defaults: {
    qeAttestations: undefined,
    registrationRequirementsStatuses: undefined,
    accommodation: undefined,
    pdReferenceLetter: undefined,
    examTitle: undefined,
    qeExamEligibility: [],
    errors: null,
  },
})
@Injectable()
export class RegistrationRequirementsState {
  constructor(
    private accommodationService: AccommodationService,
    private globalDialogService: GlobalDialogService,
    private pdReferenceLetterService: PdReferenceLetterService,
    private examinationsService: ExaminationsService,
    private qeExamEligibilityService: QeExamEligibilityService,
    private qeAttestationService: QeAttestationService,
    private qeDashboardStatusService: QeDashboardStatusService
  ) {}

  @Action(GetResgistrationRequirmentsStatuses)
  getRegistrationRequirementsStatuses(
    ctx: StateContext<IRegistrationRequirements>,
    payload: GetResgistrationRequirmentsStatuses
  ) {
    return this.qeDashboardStatusService
      .retrieveQeDashboardStatusReadOnly_GetByExamId(payload.examId)
      .pipe(
        tap((dashboardStati: IQeDashboardStatusReadOnlyModel[]) => {
          const stati: IStatuses[] = [];
          dashboardStati.forEach((status) => {
            stati.push({
              id: status.statusType,
              status: statusTypes[status.status + 1],
              disabled: status.disabled === 1 ? true : false,
            });
          });
          ctx.patchState({
            registrationRequirementsStatuses: stati,
            errors: null,
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({
            errors: errors,
          });
          return of(errors);
        })
      );
  }

  @Action(GetRegistrationRequirementsTitle)
  getRegistrationRequirementsTitle(
    ctx: StateContext<IRegistrationRequirements>,
    payload: { id: number }
  ) {
    const sessionId = payload.id;
    return this.examinationsService
      .retrieveExamTitleReadOnly_GetByExamId(sessionId)
      .pipe(
        tap((result: IExamTitleReadOnlyModel) => {
          ctx.patchState({
            examTitle: result,
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

  @Action(CreateAccommodation)
  createAccommodation(
    ctx: StateContext<IRegistrationRequirements>,
    payload: CreateAccommodation
  ) {
    return this.accommodationService.createAccommodation(payload.model).pipe(
      tap(() => {
        this.globalDialogService.showSuccessError(
          'Success',
          'Accommodation submitted successfully',
          true
        );
      }),

      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        this.globalDialogService.showSuccessError(
          'Error',
          'An error has occured while uploading the file. Please try again.',
          false
        );
        return of(errors);
      })
    );
  }

  @Action(GetAccommodations)
  getAccommodations(
    ctx: StateContext<IRegistrationRequirements>,
    payload: GetAccommodations
  ) {
    return this.accommodationService
      .retrieveAccommodation_GetByExamId(payload.examId)
      .pipe(
        tap((accommodations: IAccommodationModel) => {
          ctx.patchState({
            accommodation: accommodations,
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({
            errors: errors,
          });
          return of(errors);
        })
      );
  }

  @Action(UpdateAccommodation)
  updateAccommodations(
    ctx: StateContext<IRegistrationRequirements>,
    payload: UpdateAccommodation
  ) {
    return this.accommodationService
      .updateAccommodation(payload.examId, payload.model)
      .pipe(
        tap(() => {
          this.globalDialogService.showSuccessError(
            'Success',
            'Accommodation updated successfully',
            true
          );
        }),

        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          this.globalDialogService.showSuccessError(
            'Error',
            'An error has occured while uploading the file. Please try again.',
            false
          );
          return of(errors);
        })
      );
  }

  @Action(CreatePdReferenceLetter)
  createPdReferenceLetter(
    ctx: StateContext<IRegistrationRequirements>,
    payload: CreatePdReferenceLetter
  ) {
    return this.pdReferenceLetterService
      .createPdReferenceLetter(payload.model)
      .pipe(
        tap(() => {
          this.globalDialogService.showSuccessError(
            'Success',
            'Your reference request has been sent successfully.',
            true
          );
        }),

        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          this.globalDialogService.showSuccessError(
            'Error',
            'An error has occured. Please try again.',
            false
          );
          return of(errors);
        })
      );
  }

  @Action(GetPdReferenceLetter)
  getPdReferenceLetter(
    ctx: StateContext<IRegistrationRequirements>,
    payload: GetPdReferenceLetter
  ) {
    return this.pdReferenceLetterService
      .retrievePdReferenceLetter_GetByExamId(payload.examId)
      .pipe(
        tap((pdReferenceLetter: IPdReferenceLetterModel) => {
          const letters = [pdReferenceLetter];
          ctx.patchState({
            pdReferenceLetter: letters,
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({
            errors: errors,
          });
          return of(errors);
        })
      );
  }

  @Action(GetQeExamEligibility)
  getQeExamEligibility(ctx: StateContext<IRegistrationRequirements>) {
    return this.qeExamEligibilityService
      .retrieveQeExamEligibilityReadOnly_GetByUserId()
      .pipe(
        tap((qeExamEligibility: IQeExamEligibilityReadOnlyModel[]) => {
          const examEligibility = qeExamEligibility.map((exam) => {
            const today = new Date();
            today.setHours(0, 0, 0, 0);
            let examApplicationOpen = false;
            if (exam.appOpenDate) {
              examApplicationOpen = today >= new Date(exam.appOpenDate);
              if (exam.appCloseDate) {
                examApplicationOpen =
                  examApplicationOpen && today <= new Date(exam.appCloseDate);
              }
            }
            return {
              ...exam,
              applicationIsOpen: examApplicationOpen,
            };
          });
          ctx.patchState({
            qeExamEligibility: examEligibility,
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({
            errors: errors,
          });
          return of(errors);
        })
      );
  }

  @Action(GetQeAttestations)
  getQeAttestations(
    ctx: StateContext<IRegistrationRequirements>,
    payload: GetQeAttestations
  ) {
    return this.qeAttestationService
      .retrieveQeAttestationReadOnly_GetByExamId(payload.examId)
      .pipe(
        tap((qeAttestations: IQeAttestationReadOnlyModel[]) => {
          const attestations = [] as IAttestationReadOnlyModel[];
          qeAttestations.forEach((attestation) => {
            attestations.push({
              label: attestation.attestationText,
              name: attestation.name,
              checked: attestation.checked,
            });
          });
          ctx.patchState({
            qeAttestations: attestations,
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({
            errors: errors,
          });
          return of(errors);
        })
      );
  }

  @Action(UpdateQeAttestations)
  updateQeAttestations(
    ctx: StateContext<IRegistrationRequirements>,
    payload: UpdateQeAttestations
  ) {
    return this.qeAttestationService
      .updateQeAttestationReadOnly_UpdateByExamId(payload.examId)
      .pipe(
        tap((qeAttestations: IQeAttestationReadOnlyModel[]) => {
          this.globalDialogService.showSuccessError(
            'Success',
            'Attestation submitted successfully',
            true
          );

          const attestations = [] as IAttestationReadOnlyModel[];
          qeAttestations.forEach((attestation) => {
            attestations.push({
              label: attestation.attestationText,
              name: attestation.name,
              checked: attestation.checked,
            });
          });
          ctx.patchState({
            qeAttestations: attestations,
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          this.globalDialogService.showSuccessError(
            'Error',
            'There was an error submitting your attestation',
            false
          );
          ctx.patchState({
            errors: errors,
          });
          return of(errors);
        })
      );
  }

  @Action(ApplyForQeExam)
  applyForQeExam(
    ctx: StateContext<IRegistrationRequirements>,
    payload: ApplyForQeExam
  ) {
    return this.examinationsService
      .applyForExam_PostByExamId(payload.examId)
      .pipe(
        tap(() => {
          ctx.patchState({
            errors: null,
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({
            errors: errors,
          });
          return of(errors);
        })
      );
  }
}
