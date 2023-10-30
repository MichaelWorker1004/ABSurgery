import { IOutcomeRegistryModel } from 'src/app/api/models/continuouscertification/outcome-registry.model';

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
