import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Action, State, StateContext, StateToken } from '@ngxs/store';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { IOutcomeRegistryModel } from 'src/app/api/models/continuouscertification/outcome-registry.model';
import { OutcomeRegistriesService } from 'src/app/api/services/continuouscertification/outcome-registries.service';
import { IFormErrors } from 'src/app/shared/common';
import {
  ClearOutcomeRegistriesErrors,
  CreateOutcomeRegistries,
  GetAttestations,
  SubmitAttestation,
} from './continuous-certification.actions';
import {
  GetContinuousCertificationStatuses,
  GetOutcomeRegistries,
  GetRefrenceFormGridData,
  RequestRefrence,
  UpdateOutcomeRegistries,
} from './continuous-certification.actions';
import { IRefrenceFormReadOnlyModel } from './refrence-form-read-only.model';
import { IStatuses } from '../../api/models/users/statuses.model';
import { DashboardStatusService } from 'src/app/api/services/continuouscertification/dashboard-status.service';
import { IDashboardStatusReadOnlyModel } from 'src/app/api/models/continuouscertification/dashboard-status-read-only.model';
import { Status } from 'src/app/shared/components/action-card/status.enum';
import { IAttestationReadOnlyModel } from 'src/app/api/models/continuouscertification/attestation-read-only.model';
import { AttestationService } from 'src/app/api/services/continuouscertification/attestation.service';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';
import { IReferenceLetterReadOnlyModel } from 'src/app/api/models/continuouscertification/reference-letter-read-only.model';
import { ReferenceLetterService } from 'src/app/api/services/continuouscertification/reference-letter.service';
import { IReferenceLetterModel } from 'src/app/api/models/continuouscertification/reference-letter.model';

const statusTypes = [
  Status.InProgress, //0
  Status.Completed, //1
  Status.Contingent, //2
  Status.Alert, //3
];

export interface IContinuousCertication {
  outcomeRegistries: IOutcomeRegistryModel | undefined;
  attestations: IAttestationReadOnlyModel[] | undefined;
  continuousCertificationStatuses?: IStatuses[];
  outcomeRegistriesErrors?: IFormErrors | null;
  refrenceFormGridData?: IReferenceLetterReadOnlyModel[] | null;
  referenceLetterErrors?: IFormErrors | null;
  errors?: IFormErrors | null;
}

export const CONTCERT_STATE_TOKEN = new StateToken<IContinuousCertication>(
  'continuous_certification'
);

@State<IContinuousCertication>({
  name: CONTCERT_STATE_TOKEN,
  defaults: {
    outcomeRegistries: undefined,
    attestations: undefined,
    continuousCertificationStatuses: undefined,
    outcomeRegistriesErrors: null,
    refrenceFormGridData: null,
    referenceLetterErrors: null,
    errors: null,
  },
})
@Injectable()
export class ContinuousCertificationState {
  constructor(
    private outcomeRegistriesService: OutcomeRegistriesService,
    private dashboardStatusService: DashboardStatusService,
    private attestationService: AttestationService,
    private referenceLetterService: ReferenceLetterService,
    private globalDialogService: GlobalDialogService
  ) {}

  @Action(CreateOutcomeRegistries)
  createOutcomeRegistries(
    ctx: StateContext<IContinuousCertication>,
    { payload }: CreateOutcomeRegistries
  ) {
    return this.outcomeRegistriesService.createOutcomeRegistry(payload).pipe(
      tap((outcomeRegistries: IOutcomeRegistryModel) => {
        ctx.patchState({
          outcomeRegistries,
          outcomeRegistriesErrors: null,
        });
        this.globalDialogService.showSuccessError(
          'Success',
          'Outcome Registry saved successfully',
          true
        );
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({
          outcomeRegistriesErrors: errors,
        });
        this.globalDialogService.showSuccessError('Error', errors, false);
        return of(errors);
      })
    );
  }

  @Action(GetOutcomeRegistries)
  getOutcomeRegistries(
    ctx: StateContext<IContinuousCertication>
  ): Observable<IOutcomeRegistryModel | undefined> {
    // if (ctx.getState().outcomeRegistries) {
    //   return of(ctx.getState()?.outcomeRegistries);
    // }

    return this.outcomeRegistriesService
      .retrieveOutcomeRegistry_GetByUserId()
      .pipe(
        tap((outcomeRegistries: IOutcomeRegistryModel) => {
          ctx.patchState({
            outcomeRegistries,
            outcomeRegistriesErrors: null,
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({
            outcomeRegistriesErrors: errors,
          });
          return of(errors);
        })
      );
  }

  @Action(UpdateOutcomeRegistries)
  updateOutcomeRegistries(
    ctx: StateContext<IContinuousCertication>,
    { payload }: UpdateOutcomeRegistries
  ) {
    return this.outcomeRegistriesService.updateOutcomeRegistry(payload).pipe(
      tap((outcomeRegistries: IOutcomeRegistryModel) => {
        ctx.patchState({
          outcomeRegistries,
          outcomeRegistriesErrors: null,
        });
        ctx.dispatch(new GetOutcomeRegistries());
        this.globalDialogService.showSuccessError(
          'Success',
          'Outcome Registry saved successfully',
          true
        );
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({
          outcomeRegistriesErrors: errors,
        });
        this.globalDialogService.showSuccessError('Error', errors, false);
        return of(errors);
      })
    );
  }

  @Action(GetAttestations)
  getAttestations(ctx: StateContext<IContinuousCertication>) {
    return this.attestationService
      .retrieveAttestationReadOnly_GetAllByUserId()
      .pipe(
        tap((attestations: IAttestationReadOnlyModel[]) => {
          ctx.patchState({
            attestations,
            errors: null,
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({
            errors: errors,
          });
          this.globalDialogService.showSuccessError('Error', errors, false);
          return of(errors);
        })
      );
  }

  @Action(SubmitAttestation)
  submitAttestation(
    ctx: StateContext<IContinuousCertication>,
    { payload }: SubmitAttestation
  ) {
    return this.attestationService.submitAttestations(payload).pipe(
      tap(() => {
        ctx.patchState({
          errors: null,
        });
        ctx.dispatch(new GetAttestations());
        this.globalDialogService.showSuccessError(
          'Success',
          'Attestation submitted successfully',
          true
        );
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        ctx.patchState({
          errors: errors,
        });
        this.globalDialogService.showSuccessError('Error', errors, false);
        return of(errors);
      })
    );
  }

  @Action(GetContinuousCertificationStatuses)
  getContinuousCertificationStatuses(
    ctx: StateContext<IContinuousCertication>
  ) {
    return this.dashboardStatusService
      .retrieveDashboardStatusReadOnly_GetAllByUserId()
      .pipe(
        tap((dashboardStati: IDashboardStatusReadOnlyModel[]) => {
          const stati: IStatuses[] = [];
          dashboardStati.forEach((status) => {
            stati.push({
              id: status.statusType,
              status: statusTypes[status.status],
              disabled: status.status === 2 ? true : false,
            });
          });
          ctx.patchState({
            continuousCertificationStatuses: stati,
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

  @Action(GetRefrenceFormGridData)
  getRefrenceFormGridData(ctx: StateContext<IContinuousCertication>) {
    return this.referenceLetterService
      .retrieveReferenceLetterReadOnly_GetAllByUserId()
      .pipe(
        tap((referenceLetters: IReferenceLetterReadOnlyModel[]) => {
          const statusOptions = [
            'Required',
            'Pending',
            'Awaiting ABS Approval',
            'Approved',
          ];
          const statusClasses = ['danger', 'warning', 'warning', 'success'];

          const mappedData = referenceLetters.map((letter) => {
            return {
              ...letter,
              letterSent: letter.letterSent
                ? new Date(letter.letterSent)
                : undefined,
              statusDisplay: letter.status
                ? statusOptions[letter.status]
                : 'Processing',
              statusClass: letter.status
                ? statusClasses[letter.status]
                : 'warning',
            } as unknown as IReferenceLetterReadOnlyModel;
          });
          ctx.patchState({
            refrenceFormGridData: mappedData,
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

  @Action(RequestRefrence)
  requestRefrence(
    ctx: StateContext<IContinuousCertication>,
    payload: RequestRefrence
  ) {
    return this.referenceLetterService
      .createReferenceLetter(payload.model)
      .pipe(
        tap((response) => {
          ctx.patchState({ referenceLetterErrors: null });
          ctx.dispatch(new GetRefrenceFormGridData());
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
          ctx.patchState({ referenceLetterErrors: errors });
          return of(errors);
        })
      );
  }

  @Action(ClearOutcomeRegistriesErrors)
  clearOutcomeRegistriesErrors(ctx: StateContext<IContinuousCertication>) {
    ctx.patchState({
      outcomeRegistriesErrors: null,
    });
  }
}
