import { IAccommodationModel } from 'src/app/api/models/examinations/accommodation.model';
import { IPdReferenceLetterModel } from 'src/app/api/models/examinations/pd-reference-letter.model';

export class GetResgistrationRequirmentsStatuses {
  static readonly type = '[RegistrationRequirements] Get Statuses';
  constructor(public examId: number) {}
}

export class CreateAccommodation {
  static readonly type = '[RegistrationRequirements] Create Accommodation';
  constructor(public model: IAccommodationModel) {}
}

export class GetAccommodations {
  static readonly type = '[RegistrationRequirements] Get Accommodations';
  constructor(public examId: number) {}
}

export class UpdateAccommodation {
  static readonly type = '[RegistrationRequirements] Update Accommodations';
  constructor(public examId: number, public model: IAccommodationModel) {}
}

export class CreatePdReferenceLetter {
  static readonly type =
    '[RegistrationRequirements] Create Pd Reference Letter';
  constructor(public model: IPdReferenceLetterModel) {}
}

export class GetPdReferenceLetter {
  static readonly type = '[RegistrationRequirements] Get Pd Reference Letter';
  constructor(public examId: number) {}
}

export class GetRegistrationRequirementsTitle {
  static readonly type = '[RegistrationRequirements] Get Title';
  constructor(public id: number) {}
}

export class GetQeExamEligibility {
  static readonly type = '[RegistrationRequirements] Get QW Eligibility';
}

export class GetQeAttestations {
  static readonly type = '[RegistrationRequirements] Get QE Attestations';
  constructor(public examId: number) {}
}

export class UpdateQeAttestations {
  static readonly type = '[RegistrationRequirements] Update QE Attestations';
  constructor(public examId: number) {}
}

export class ApplyForQeExam {
  static readonly type = '[RegistrationRequirements] Apply For QE Exam';
  constructor(public examId: number) {}
}

export class CompleteExamRegistration {
  static readonly type =
    '[RegistrationRequirements] Complete Exam Registration';
  constructor(public examId: number) {}
}
