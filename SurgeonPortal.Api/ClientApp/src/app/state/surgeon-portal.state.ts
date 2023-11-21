import { AuthState } from './auth';
import { DashboardState } from './dashboard';
import { MyAccountState } from './my-account';
import { UserProfileState } from './user-profile';
import { PicklistsState } from './picklists/picklists.state';
import { ContinuousCertificationState } from './continuous-certification';
import { GQAdditionalTrainingState } from './examination-gq-additional-training';
import { MedicalTrainingState } from './medical-training';
import { GraduateMedicalEducationState } from './graduatemedicaleducation';
import { DocumentsState } from './documents';
import { ExamProcessState } from './exam-process';
import { ProfessionalStandingState } from './professional-standing';
import { ExamScoringState } from './exam-scoring';
import { ContinuingMedicalEducationState } from './continuingmedicaleducation';
import { ExamHistoryState } from './exam-history';
import { ApplicationState } from './application/application.state';
import { RegistrationRequirementsState } from './registration-requirements';

export const surgeonPortalState = [
  ApplicationState,
  AuthState,
  MyAccountState,
  UserProfileState,
  PicklistsState,
  DashboardState,
  ContinuousCertificationState,
  GQAdditionalTrainingState,
  MedicalTrainingState,
  GraduateMedicalEducationState,
  DocumentsState,
  ExamProcessState,
  ProfessionalStandingState,
  ExamScoringState,
  ContinuingMedicalEducationState,
  ExamScoringState,
  ExamHistoryState,
  RegistrationRequirementsState,
];
