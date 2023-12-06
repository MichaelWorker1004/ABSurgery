import { IOutcomeRegistryModel } from 'src/app/api/models/continuouscertification/outcome-registry.model';
import { IRefrenceFormModel } from './refrence-form.model';
import { IAttestationSubmitModel } from 'src/app/api/models/continuouscertification/attestation-read-only.model';

export class CreateOutcomeRegistries {
  static readonly type = '[Continuous Certification] Create Outcome Registries';

  constructor(public payload: IOutcomeRegistryModel) {}
}

export class GetOutcomeRegistries {
  static readonly type = '[Continuous Certification] Get Outcome Registries';
}

export class UpdateOutcomeRegistries {
  static readonly type = '[Continuous Certification] Update Outcome Registries';

  constructor(public payload: IOutcomeRegistryModel) {}
}

export class GetAttestations {
  static readonly type = '[Continuous Certification] Get Attestations';
}

export class SubmitAttestation {
  static readonly type = '[Continuous Certification] Submit Attestation';

  constructor(public payload: IAttestationSubmitModel) {}
}

export class GetContinuousCertificationStatuses {
  static readonly type =
    '[Continuous Certification] Get Continuous Certification Statuses';
}

export class GetRefrenceFormGridData {
  static readonly type =
    '[Continuous Certification] Get Reference Form Grid Data';
}

export class RequestRefrence {
  static readonly type =
    '[Continuous Certification] Request Reference Form Grid Data';

  constructor(public model: IRefrenceFormModel) {}
}

export class ClearOutcomeRegistriesErrors {
  static readonly type =
    '[Continuous Certification] Clear Outcome Registries Errors';
}
