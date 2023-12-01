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
  GetAttestations,
} from './continuous-certification.actions';
import {
  GetContinuousCertificationStatuses,
  GetOutcomeRegistries,
  GetRefrenceFormGridData,
  RequestRefrence,
  UpdateOutcomeRegistries,
} from './continuous-certification.actions';
import { IAttestationModel } from './attestation.model';
import { IRefrenceFormReadOnlyModel } from './refrence-form-read-only.model';
import { IStatuses } from '../../api/models/users/statuses.model';
import { DashboardStatusService } from 'src/app/api/services/continuouscertification/dashboard-status.service';
import { IDashboardStatusReadOnlyModel } from 'src/app/api/models/continuouscertification/dashboard-status-read-only.model';
import { Status } from 'src/app/shared/components/action-card/status.enum';

const statusTypes = [
  Status.InProgress, //0
  Status.Completed, //1
  Status.Contingent, //2
  Status.Alert, //3
];

export interface IContinuousCertication {
  outcomeRegistries: IOutcomeRegistryModel | undefined;
  attestations: IAttestationModel[] | undefined;
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
    private dashboardStatusService: DashboardStatusService
  ) {}

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
    ctx.patchState({
      outcomeRegistries: payload,
    });

    return this.outcomeRegistriesService.updateOutcomeRegistry(payload).pipe(
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

  @Action(GetAttestations)
  getAttestations(ctx: StateContext<IContinuousCertication>) {
    ctx.patchState({
      attestations: [
        {
          label:
            'I hereby authorize any hospital or medical staff where I now have,have had, or have applied for medical staff privileges, and anymedical organization of which I am a member or to which I have applied for membership, and any person who may have information (including medical records, patient records, and reports of committees) which is deemed by ABS to be material to its evaluation of this application, to provide such information to representatives of the ABS. I agree that communications of any nature made to the ABS regarding this application may be made in confidence and shall not be made available to me under any circumstances. I hereby release from liability any hospital. medical staff, medical organization or person, and ABS and its representatives, for acts performed in connection with this application.',
          name: 'attestation1',
          checked: false,
        },
        {
          label:
            'I understand that the certificate I will be issued upon successful completion of the biennial Continuous Certification Assessment will be contingent upon my on-going active participation in the Continuous Certification Program as a whole. I recognize that 10-year certificates are no longer offered by the ABS, and that the biennial Continuous Certification Assessment is replacing the traditional 10-vear recertification examination.',
          name: 'attestation2',
          checked: true,
        },
        {
          label: 'Some other Attestation',
          name: 'attestation3',
          checked: false,
        },
      ],
    });
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
}
