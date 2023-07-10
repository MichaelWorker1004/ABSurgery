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
import { ProfessionalStandingState } from './professional-standing';
import { ExamScoringState } from './exam-scoring';

export const surgeonPortalState = [
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
  ProfessionalStandingState,
  ExamScoringState,
];
