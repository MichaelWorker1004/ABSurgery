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
import { ct } from '@fullcalendar/core/internal-common';
//remove this
import { ApiService } from 'ytg-angular';

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
  refrenceFormGridData?: IRefrenceFormReadOnlyModel[] | null;
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
    errors: null,
  },
})
@Injectable()
export class ContinuousCertificationState {
  constructor(
    private outcomeRegistriesService: OutcomeRegistriesService,
    private dashboardStatusService: DashboardStatusService,
    private attestationService: AttestationService,
    private globalDialogService: GlobalDialogService,
    //remove this
    private apiService: ApiService
  ) {}

  @Action(CreateOutcomeRegistries)
  createOutcomeRegistries(
    ctx: StateContext<IContinuousCertication>,
    { payload }: CreateOutcomeRegistries
  ) {
    console.log(payload);
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
    //remove this
    this.checkAPIAddress();
    return this.dashboardStatusService
      .retrieveDashboardStatusReadOnly_GetAllByUserId()
      .pipe(
        tap((dashboardStati: IDashboardStatusReadOnlyModel[]) => {
          const stati: IStatuses[] = [];
          dashboardStati.forEach((status) => {
            stati.push({
              id: status.statusType,
              status: statusTypes[status.status],
              disabled: status.status === 3 ? true : false,
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
    const response = [
      {
        referenceFormId: 'MD19143',
        affiliatedInstitution: 'ABS',
        authenticatingOfficial: 'John Doe, M.D.',
        date: new Date('09/21/2019'),
        status: 'Requested',
      },
      {
        referenceFormId: 'MD08221',
        affiliatedInstitution: 'ABS',
        authenticatingOfficial: 'Mary Joseph',
        date: new Date('08/12/2019'),
        status: 'Approved',
      },
      {
        referenceFormId: 'MD12345',
        affiliatedInstitution: 'ABS',
        authenticatingOfficial: 'John Dorian',
        date: new Date('8/1/2019'),
        status: 'Approved',
      },
    ];

    ctx.patchState({
      refrenceFormGridData: response,
    });
  }

  @Action(RequestRefrence)
  requestRefrence(
    ctx: StateContext<IContinuousCertication>,
    { model }: RequestRefrence
  ) {
    // API CALL TO SEND REFRENCE
    console.log('Request Refrence', model);

    // REFRESH GRID DATA
    ctx.dispatch(new GetRefrenceFormGridData());
  }

  @Action(ClearOutcomeRegistriesErrors)
  clearOutcomeRegistriesErrors(ctx: StateContext<IContinuousCertication>) {
    ctx.patchState({
      outcomeRegistriesErrors: null,
    });
  }

  // remove this
  private checkAPIAddress(): void {
    this.apiService
      .get(`api/billing/transaction/getip`)
      .subscribe((res: any) => {
        console.log('api-address', res);
      });
  }
}
