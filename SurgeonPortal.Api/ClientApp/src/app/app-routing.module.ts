import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FileNotFoundComponent } from './file-not-found/file-not-found.component';
import { AuthGuard, FeatureToggleGuard } from './state';
import { PersonalProfileComponent } from './personal-profile/personal-profile.component';
import { MedicalTrainingComponent } from './medical-training/medical-training.component';
import { ProfessionalStandingComponent } from './professional-standing/professional-standing.component';
import { MyAccountComponent } from './my-account/my-account.component';
import { OralExaminationsComponent } from './oral-examination/oral-examination.component';
import { UserClaims } from './side-navigation/user-status.enum';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';

const canDeactivate = async (component: any, title?: string, text?: string) => {
  if (component?.hasUnsavedChanges) {
    const result = await component.globalDialogService?.showConfirmation(
      title ?? 'Unsaved Changes',
      text ?? 'Do you want to navigate away'
    );

    return result;
  }

  return true;
};

const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full',
  },
  {
    path: 'login',
    loadComponent: () =>
      import('./login/login.component').then((m) => m.LoginComponent),
  },
  {
    path: 'dashboard',
    loadComponent: () =>
      import('./dashboard/dashboard.component').then(
        (m) => m.DashboardComponent
      ),
    data: { requiredFeatures: ['dashboardPage'] },
    canActivate: [AuthGuard, FeatureToggleGuard],
  },
  {
    path: 'cme-repository',
    loadComponent: () =>
      import('./cme-repository/cme-repository.component').then(
        (m) => m.CmeRepositoryComponent
      ),
    canActivate: [AuthGuard, FeatureToggleGuard],
    data: {
      requiredClaims: [UserClaims.surgeon],
      requiredFeatures: ['cmeRepositoryPage'],
    },
  },
  {
    path: 'personal-profile',
    loadComponent: () =>
      import('./personal-profile/personal-profile.component').then(
        (m) => m.PersonalProfileComponent
      ),
    canActivate: [AuthGuard, FeatureToggleGuard],
    data: {
      requiredClaims: [UserClaims.user],
      requiredFeatures: ['personalProfilePage'],
    },
    canDeactivate: [
      (component: PersonalProfileComponent) => canDeactivate(component),
    ],
  },
  {
    path: 'medical-training',
    loadComponent: () =>
      import('./medical-training/medical-training.component').then(
        (m) => m.MedicalTrainingComponent
      ),
    canActivate: [AuthGuard, FeatureToggleGuard],
    data: {
      requiredClaims: [UserClaims.user],
      requiredFeatures: ['medicalTrainingPage'],
    },
    canDeactivate: [
      (component: MedicalTrainingComponent) => canDeactivate(component),
    ],
  },
  {
    path: 'professional-standing',
    loadComponent: () =>
      import('./professional-standing/professional-standing.component').then(
        (m) => m.ProfessionalStandingComponent
      ),
    canActivate: [AuthGuard, FeatureToggleGuard],
    data: {
      requiredClaims: [UserClaims.surgeon],
      requiredFeatures: ['professionalStandingPage'],
    },
    canDeactivate: [
      (component: ProfessionalStandingComponent) => canDeactivate(component),
    ],
  },
  {
    path: 'apply-and-resgister',
    loadComponent: () =>
      import('./exam-process/exam-process.component').then(
        (m) => m.ExamProcessComponent
      ),
    canActivate: [AuthGuard, FeatureToggleGuard],
    data: {
      requiredClaims: [UserClaims.surgeon],
      requiredFeatures: ['applyRegisterPage'],
    },
  },
  {
    path: 'apply-and-resgister/registration-requirements',
    loadComponent: () =>
      import(
        './registration-requirements/registration-requirements.component'
      ).then((m) => m.RegistrationRequirementsComponent),
    canActivate: [AuthGuard, FeatureToggleGuard],
    data: {
      requiredClaims: [UserClaims.surgeon],
      requiredFeatures: ['applyRegisterPage'],
    },
  },
  {
    path: 'apply-and-resgister/exam-registration',
    loadComponent: () =>
      import('./exam-registration/exam-registration.component').then(
        (m) => m.ExamRegistrationComponent
      ),
    canActivate: [AuthGuard, FeatureToggleGuard],
    data: {
      requiredClaims: [UserClaims.surgeon],
      requiredFeatures: ['applyRegisterPage'],
    },
  },
  {
    path: 'examination-history',
    loadComponent: () =>
      import('./examination-history/examination-history.component').then(
        (m) => m.ExaminationHistoryComponent
      ),
    canActivate: [AuthGuard, FeatureToggleGuard],
    data: {
      requiredClaims: [UserClaims.surgeon],
      requiredFeatures: ['examHistoryPage'],
    },
  },
  {
    path: 'continuous-certification',
    loadComponent: () =>
      import(
        './continuous-certification/continuous-certification.component'
      ).then((m) => m.ContinuousCertificationComponent),
    canActivate: [AuthGuard, FeatureToggleGuard],
    data: {
      requiredClaims: [UserClaims.surgeon],
      requiredFeatures: ['continuousCertificationPage'],
    },
  },
  {
    path: 'payment-history',
    loadComponent: () =>
      import('./payment-history/payment-history.component').then(
        (m) => m.PaymentHistoryComponent
      ),
    canActivate: [AuthGuard, FeatureToggleGuard],
    data: {
      requiredClaims: [UserClaims.surgeon],
      requiredFeatures: ['paymentHistoryPage'],
    },
  },
  {
    path: 'documents',
    loadComponent: () =>
      import('./documents/documents.component').then(
        (m) => m.DocumentsComponent
      ),
    canActivate: [AuthGuard, FeatureToggleGuard],
    data: {
      requiredClaims: [UserClaims.user],
      requiredFeatures: ['documentsPage'],
    },
  },
  {
    path: 'committees',
    loadComponent: () =>
      import('./committees/committees.component').then(
        (m) => m.CommitteesComponent
      ),
    canActivate: [AuthGuard, FeatureToggleGuard],
    data: {
      requiredClaims: [UserClaims.surgeon],
      requiredFeatures: ['committeesPage'],
    },
  },
  {
    path: 'my-account',
    loadComponent: () =>
      import('./my-account/my-account.component').then(
        (m) => m.MyAccountComponent
      ),
    canActivate: [AuthGuard, FeatureToggleGuard],
    data: {
      requiredClaims: [UserClaims.user],
      requiredFeatures: ['myAccountPage'],
    },
    canDeactivate: [
      (component: MyAccountComponent) => canDeactivate(component),
    ],
  },
  {
    path: 'gme-history',
    loadComponent: () =>
      import('./gme-history/gme-history.component').then(
        (m) => m.GmeHistoryComponent
      ),
    canActivate: [AuthGuard, FeatureToggleGuard],
    data: {
      requiredClaims: [UserClaims.trainee],
      requiredFeatures: ['gmeHistoryPage'],
    },
  },
  {
    path: 'ce-scoring',
    loadComponent: () =>
      import('./ce-scoring/ce-scoring.component').then(
        (m) => m.CeScoringAppComponent
      ),
    canActivate: [AuthGuard, FeatureToggleGuard],
    data: {
      requiredClaims: [UserClaims.examiner],
      requiredFeatures: ['examScoringPage'],
    },
  },
  {
    path: 'ce-scoring/examination-rosters',
    loadComponent: () =>
      import('./examination-rosters/examination-rosters.component').then(
        (m) => m.ExaminationRostersComponent
      ),
    canActivate: [AuthGuard, FeatureToggleGuard],
    data: {
      requiredClaims: [UserClaims.examiner],
      requiredFeatures: ['examScoringPage'],
    },
  },
  {
    path: 'ce-scoring/oral-examinations',
    loadComponent: () =>
      import('./oral-examinations/oral-examinations.component').then(
        (m) => m.OralExaminationsComponent
      ),
    canActivate: [AuthGuard, FeatureToggleGuard],
    data: {
      requiredClaims: [UserClaims.examiner],
      requiredFeatures: ['examScoringPage'],
    },
  },
  {
    path: 'ce-scoring/oral-examinations/exam/:examinationId',
    loadComponent: () =>
      import('./oral-examination/oral-examination.component').then(
        (m) => m.OralExaminationsComponent
      ),
    canActivate: [AuthGuard, FeatureToggleGuard],
    data: {
      requiredClaims: [UserClaims.examiner],
      requiredFeatures: ['examScoringPage'],
    },
    canDeactivate: [
      (component: OralExaminationsComponent) =>
        canDeactivate(
          component,
          'Exam in Process',
          'Do you want to navigate away from the exam? <br/> Navigating away will result in an incomplete exam.'
        ),
    ],
  },
  {
    path: 'ce-scoring/examination-scores',
    loadComponent: () =>
      import('./examination-scores/examination-scores.component').then(
        (m) => m.ExaminationScoresComponent
      ),
    canActivate: [AuthGuard, FeatureToggleGuard],
    data: {
      requiredClaims: [UserClaims.examiner],
      requiredFeatures: ['examScoringPage'],
    },
  },
  {
    path: 'unauthorized',
    component: UnauthorizedComponent,
  },
  {
    path: '**',
    component: FileNotFoundComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
