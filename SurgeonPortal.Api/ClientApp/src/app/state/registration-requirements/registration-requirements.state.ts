import { Injectable } from '@angular/core';
import { Action, State, StateContext, StateToken } from '@ngxs/store';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { IAccommodationModel } from 'src/app/api/models/examinations/accommodation.model';
import { IStatuses } from 'src/app/api/models/users/statuses.model';
import { AccommodationService } from 'src/app/api/services/examinations/accommodation.service';
import { IFormErrors } from 'src/app/shared/common';
import {
  CreateAccommodation,
  GetAccommodations,
  GetResgistrationRequirmentsStatuses,
} from './registration-requirements.actions';
import { HttpErrorResponse } from '@angular/common/http';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';

export interface IRegistrationRequirements {
  registrationRequirementsStatuses?: IStatuses[];
  accommodation?: IAccommodationModel;
  errors?: IFormErrors | null;
}

export const REGREQ_STATE_TOKEN = new StateToken<IRegistrationRequirements>(
  'registration_requirements'
);

@State<IRegistrationRequirements>({
  name: REGREQ_STATE_TOKEN,
  defaults: {
    registrationRequirementsStatuses: undefined,
    accommodation: undefined,
    errors: null,
  },
})
@Injectable()
export class RegistrationRequirementsState {
  constructor(
    private accommodationService: AccommodationService,
    private globalDialogService: GlobalDialogService
  ) {}

  @Action(GetResgistrationRequirmentsStatuses)
  getRegistrationRequirementsStatuses(
    ctx: StateContext<IRegistrationRequirements>
  ) {
    const response: IStatuses[] = [
      {
        id: 'personalProfile',
        status: 'completed',
        disabled: false,
      },
      {
        id: 'training',
        status: 'completed',
        disabled: false,
      },
      {
        id: 'personalActivities',
        status: 'completed',
        disabled: false,
      },
      {
        id: 'medicalLicense',
        status: 'completed',
        disabled: false,
      },
      {
        id: 'acgmeExperience',
        status: 'completed',
        disabled: false,
      },
      {
        id: 'gme',
        status: 'alert',
        disabled: false,
      },
      {
        id: 'attestation',
        status: 'in-progress',
        disabled: false,
      },
      {
        id: 'certifications',
        status: 'completed',
        disabled: false,
      },
      {
        id: 'applicationFee',
        status: 'completed',
        disabled: false,
      },
      {
        id: 'accommodations',
        status: 'completed',
        disabled: false,
      },
    ];

    ctx.patchState({
      registrationRequirementsStatuses: response,
    });
  }

  @Action(CreateAccommodation)
  createAccommodation(
    ctx: StateContext<IRegistrationRequirements>,
    payload: CreateAccommodation
  ) {
    return this.accommodationService.createAccommodation(payload.model).pipe(
      tap(() => {
        // fetch accommodations
        ctx.dispatch(new GetAccommodations(payload.model.examId));
        this.globalDialogService.showSuccessError(
          'Success',
          'Accommodation saved successfully',
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
}
