import { IAdvancedTrainingModel } from 'src/app/api/models/medicaltraining/advanced-training.model';
import { IFellowshipModel } from 'src/app/api/models/medicaltraining/fellowship.model';
import { IMedicalTrainingModel } from 'src/app/api/models/medicaltraining/medical-training.model';
import { IOtherCertificationsModel } from 'src/app/api/models/medicaltraining/other-certifications.model';

export class GetMedicalTraining {
  static readonly type = '[Medical Training] Get medical school data';
}

export class CreateMedicalTraining {
  static readonly type = '[Medical Training] Create medical school data';
  constructor(public payload: IMedicalTrainingModel) {}
}

export class UpdateMedicalTraining {
  static readonly type = '[Medical Training] Update medical school data';
  constructor(public payload: IMedicalTrainingModel) {}
}

export class GetAdvancedTrainingData {
  static readonly type = '[Medical Training] Get advanced training data';
}

export class UpdateAdvancedTrainingData {
  static readonly type = '[Medical Training] Update advanced training Data';

  constructor(public payload: IAdvancedTrainingModel) {}
}

export class GetUserCertificates {
  static readonly type = '[Medical Training] Get user certificates';

  constructor(public isUpload?: boolean) {}
}

export class GetOtherCertifications {
  static readonly type = '[Medical Training] Get user other certifications';

  constructor(public isUpdate?: boolean) {}
}

export class CreateOtherCertification {
  static readonly type = '[Medical Training] Create user other certifications';

  constructor(public model: IOtherCertificationsModel) {}
}

export class UpdateOtherCertifications {
  static readonly type = '[Medical Training] Update user other certifications';

  constructor(public model: IOtherCertificationsModel) {}
}

export class GetFellowships {
  static readonly type = '[Medical Training] Get fellowships';

  constructor(public isUpdate?: boolean) {}
}

export class CreateFellowship {
  static readonly type = '[Medical Training] Create fellowships';

  constructor(public model: IFellowshipModel) {}
}

export class UpdateFellowship {
  static readonly type = '[Medical Training] Update fellowships';

  constructor(public model: IFellowshipModel) {}
}

export class DeleteFellowship {
  static readonly type = '[Medical Training] Delete fellowships';

  constructor(public id: number) {}
}

export class ClearMedicalTrainingErrors {
  static readonly type = '[Medical Training] Clear medical training errors';
}

export class CreateAdvancedTraining {
  static readonly type = '[Medical Training] Create advanced training data';

  constructor(public model: IAdvancedTrainingModel) {}
}

export class UpdateAdvancedTraining {
  static readonly type = '[Medical Training] Update advanced training data';

  constructor(public model: IAdvancedTrainingModel) {}
}
