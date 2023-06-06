import { Injectable } from '@angular/core';
import { catchError, share, tap } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { forkJoin, map, Observable, of } from 'rxjs';
import { Action, State, StateContext, StateToken, Store } from '@ngxs/store';
import { OutcomeRegistriesService } from 'src/app/api/services/continuouscertification/outcome-registries.service';
import { IOutcomeRegistryModel } from 'src/app/api/models/continuouscertification/outcome-registry.model';
import {
  GetOutcomeRegistries,
  UpdateOutcomeRegistries,
} from './continuous-certification.actions';
import { IFormErrors } from 'src/app/shared/common';

export interface IContinuousCertication {
  outcomeRegistries?: IOutcomeRegistryModel;
  errors?: IFormErrors | null;
}

export const CONTCERT_STATE_TOKEN = new StateToken<IContinuousCertication>(
  'continuous_certification'
);

@State<IContinuousCertication>({
  name: CONTCERT_STATE_TOKEN,
  defaults: {
    outcomeRegistries: undefined,
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
          });
        }),
        catchError((httpError: HttpErrorResponse) => {
          const errors = httpError.error;
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
        });
      }),
      catchError((httpError: HttpErrorResponse) => {
        const errors = httpError.error;
        return of(errors);
      })
    );
  }
}
