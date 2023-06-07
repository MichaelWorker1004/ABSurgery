import { IAdvancedTrainingModel } from 'src/app/api/models/medicaltraining/advanced-training.model';
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
