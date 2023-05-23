import { IAdvancedTrainingModel } from 'src/app/api/models/medicaltraining/advanced-training.model';
import { IMedicalTrainingModel } from 'src/app/api/models/medicaltraining/medical-training.model';

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
