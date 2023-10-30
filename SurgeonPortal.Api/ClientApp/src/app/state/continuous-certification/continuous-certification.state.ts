import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Action, State, StateContext, StateToken } from '@ngxs/store';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { IOutcomeRegistryModel } from 'src/app/api/models/continuouscertification/outcome-registry.model';
import { OutcomeRegistriesService } from 'src/app/api/services/continuouscertification/outcome-registries.service';
import { IFormErrors } from 'src/app/shared/common';
import {
  GetAttestations,
  GetOutcomeRegistries,
  UpdateOutcomeRegistries,
} from './continuous-certification.actions';
import { IAttestationModel } from './attestation.model';

export interface IContinuousCertication {
  outcomeRegistries: IOutcomeRegistryModel | undefined;
  attestations: IAttestationModel[] | undefined;
  errors: IFormErrors | null;
}

export const CONTCERT_STATE_TOKEN = new StateToken<IContinuousCertication>(
  'continuous_certification'
);

@State<IContinuousCertication>({
  name: CONTCERT_STATE_TOKEN,
  defaults: {
    outcomeRegistries: undefined,
    attestations: undefined,
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
}
