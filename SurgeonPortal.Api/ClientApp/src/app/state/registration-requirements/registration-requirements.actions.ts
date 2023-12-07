import { IAccommodationModel } from 'src/app/api/models/examinations/accommodation.model';

export class GetResgistrationRequirmentsStatuses {
  static readonly type = '[RegistrationRequirements] Get Statuses';
}

export class CreateAccommodation {
  static readonly type = '[RegistrationRequirements] Create Accommodation';
  constructor(public model: IAccommodationModel) {}
}
