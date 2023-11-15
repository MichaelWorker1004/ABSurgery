import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Action, State, StateContext, StateToken } from '@ngxs/store';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { IOutcomeRegistryModel } from 'src/app/api/models/continuouscertification/outcome-registry.model';
import { OutcomeRegistriesService } from 'src/app/api/services/continuouscertification/outcome-registries.service';
import { IFormErrors } from 'src/app/shared/common';
import {
  GetContinuousCertificationStatuses,
  GetOutcomeRegistries,
  UpdateOutcomeRegistries,
} from './continuous-certification.actions';
import { IContinuousCerticationStatuses } from './continuous-certification-statuses.model';

export interface IContinuousCertication {
  outcomeRegistries?: IOutcomeRegistryModel;
  continuousCertificationStatuses?: IContinuousCerticationStatuses;
  outcomeRegistriesErrors?: IFormErrors | null;
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
}
