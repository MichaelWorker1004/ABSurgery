import { IOutcomeRegistryModel } from 'src/app/api/models/continuouscertification/outcome-registry.model';

export class GetOutcomeRegistries {
  static readonly type = '[Continuous Certification] Get Outcome Registries';

  constructor(public userId: number) {}
}

export class UpdateOutcomeRegistries {
  static readonly type = '[Continuous Certification] Update Outcome Registries';

  constructor(public payload: IOutcomeRegistryModel) {}
}
