import { Injectable } from '@angular/core';
import { Action, State, StateContext, StateToken } from '@ngxs/store';
import { IFormErrors } from 'src/app/shared/common';
import { GetResgistrationRequirmentsStatuses } from './registration-requirements.actions';
import { IStatuses } from 'src/app/api/models/users/statuses.model';

export interface IRegistrationRequirements {
  registrationRequirementsStatuses?: IStatuses[];
  erros?: IFormErrors | null;
}

export const REGREQ_STATE_TOKEN = new StateToken<IRegistrationRequirements>(
  'registration_requirements'
);

@State<IRegistrationRequirements>({
  name: REGREQ_STATE_TOKEN,
  defaults: {
    registrationRequirementsStatuses: undefined,
    erros: null,
  },
})
@Injectable()
export class RegistrationRequirementsState {
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
}
