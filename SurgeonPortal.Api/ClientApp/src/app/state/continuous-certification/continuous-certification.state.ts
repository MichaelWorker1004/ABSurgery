import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Action, State, StateContext, StateToken } from '@ngxs/store';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { IOutcomeRegistryModel } from 'src/app/api/models/continuouscertification/outcome-registry.model';
import { OutcomeRegistriesService } from 'src/app/api/services/continuouscertification/outcome-registries.service';
import { IFormErrors } from 'src/app/shared/common';
import { IContinuousCerticationStatuses } from './continuous-certification-statuses.model';
import {
  GetContinuousCertificationStatuses,
  GetOutcomeRegistries,
  GetRefrenceFormGridData,
  RequestRefrence,
  UpdateOutcomeRegistries,
} from './continuous-certification.actions';
import { IRefrenceFormReadOnlyModel } from './refrence-form-read-only.model';

export interface IContinuousCertication {
  outcomeRegistries?: IOutcomeRegistryModel;
  continuousCertificationStatuses?: IContinuousCerticationStatuses;
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
    continuousCertificationStatuses: undefined,
    outcomeRegistriesErrors: null,
    refrenceFormGridData: null,
    errors: null,
  },
})
@Injectable()
export class ContinuousCertificationState {
  constructor(private outcomeRegistriesService: OutcomeRegistriesService) {}

  @Action(GetOutcomeRegistries)
  getOutcomeRegistries(
    ctx: StateContext<IContinuousCertication>
  ): Observable<IOutcomeRegistryModel | undefined> {
    if (ctx.getState().outcomeRegistries) {
      return of(ctx.getState()?.outcomeRegistries);
    }

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

  @Action(GetContinuousCertificationStatuses)
  getContinuousCertificationStatuses(
    ctx: StateContext<IContinuousCertication>
  ) {
    const response = {
      personalProfile: {
        status: 'completed',
        disabled: false,
      },
      outcomeRegistries: {
        status: 'completed',
        disabled: false,
      },
      medicalTraining: {
        status: 'completed',
        disabled: false,
      },
      professionalStanding: {
        status: 'completed',
        disabled: false,
      },
      cmeRepository: {
        status: 'completed',
        disabled: false,
      },
      payFee: {
        status: 'completed',
        disabled: false,
      },
      referenceForms: {
        status: 'completed',
        disabled: false,
      },
      attestation: {
        status: 'in-progress',
        disabled: false,
      },
    };

    ctx.patchState({
      continuousCertificationStatuses: response,
    });
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
}
