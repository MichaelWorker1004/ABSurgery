import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FileNotFoundComponent } from './file-not-found/file-not-found.component';
import { AuthGuard } from './state';
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
    canActivate: [AuthGuard],
  },
  {
    path: 'cme-repository',
    loadComponent: () =>
      import('./cme-repository/cme-repository.component').then(
        (m) => m.CmeRepositoryComponent
      ),
    canActivate: [AuthGuard],
    data: { requiredClaims: [UserClaims.surgeon] },
  },
  {
    path: 'personal-profile',
    loadComponent: () =>
      import('./personal-profile/personal-profile.component').then(
        (m) => m.PersonalProfileComponent
      ),
    canActivate: [AuthGuard],
    data: { requiredClaims: [UserClaims.user] },
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
    canActivate: [AuthGuard],
    data: { requiredClaims: [UserClaims.user] },
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
    canActivate: [AuthGuard],
    data: { requiredClaims: [UserClaims.surgeon] },
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
    canActivate: [AuthGuard],
    data: { requiredClaims: [UserClaims.surgeon] },
  },
  {
    path: 'apply-and-resgister/registration-requirements',
    loadComponent: () =>
      import(
        './registration-requirements/registration-requirements.component'
      ).then((m) => m.RegistrationRequirementsComponent),
    canActivate: [AuthGuard],
    data: { requiredClaims: [UserClaims.surgeon] },
  },
  {
    path: 'apply-and-resgister/exam-registration',
    loadComponent: () =>
      import('./exam-registration/exam-registration.component').then(
        (m) => m.ExamRegistrationComponent
      ),
    canActivate: [AuthGuard],
    data: { requiredClaims: [UserClaims.surgeon] },
  },
  {
    path: 'examination-history',
    loadComponent: () =>
      import('./examination-history/examination-history.component').then(
        (m) => m.ExaminationHistoryComponent
      ),
    canActivate: [AuthGuard],
    data: { requiredClaims: [UserClaims.surgeon] },
  },
  {
    path: 'continuous-certification',
    loadComponent: () =>
      import(
        './continuous-certification/continuous-certification.component'
      ).then((m) => m.ContinuousCertificationComponent),
    canActivate: [AuthGuard],
    data: { requiredClaims: [UserClaims.surgeon] },
  },
  {
    path: 'payment-history',
    loadComponent: () =>
      import('./payment-history/payment-history.component').then(
        (m) => m.PaymentHistoryComponent
      ),
    canActivate: [AuthGuard],
    data: { requiredClaims: [UserClaims.surgeon] },
  },
  {
    path: 'documents',
    loadComponent: () =>
      import('./documents/documents.component').then(
        (m) => m.DocumentsComponent
      ),
    canActivate: [AuthGuard],
    data: { requiredClaims: [UserClaims.user] },
  },
  {
    path: 'committees',
    loadComponent: () =>
      import('./committees/committees.component').then(
        (m) => m.CommitteesComponent
      ),
    canActivate: [AuthGuard],
    data: { requiredClaims: [UserClaims.surgeon] },
  },
  {
    path: 'my-account',
    loadComponent: () =>
      import('./my-account/my-account.component').then(
        (m) => m.MyAccountComponent
      ),
    canActivate: [AuthGuard],
    data: { requiredClaims: [UserClaims.user] },
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
    canActivate: [AuthGuard],
    data: { requiredClaims: [UserClaims.trainee] },
  },
  {
    path: 'ce-scoring',
    loadComponent: () =>
      import('./ce-scoring/ce-scoring.component').then(
        (m) => m.CeScoringAppComponent
      ),
    canActivate: [AuthGuard],
    data: { requiredClaims: [UserClaims.examiner] },
  },
  {
    path: 'ce-scoring/examination-rosters',
    loadComponent: () =>
      import('./examination-rosters/examination-rosters.component').then(
        (m) => m.ExaminationRostersComponent
      ),
    canActivate: [AuthGuard],
    data: { requiredClaims: [UserClaims.examiner] },
  },
  {
    path: 'ce-scoring/oral-examinations',
    loadComponent: () =>
      import('./oral-examinations/oral-examinations.component').then(
        (m) => m.OralExaminationsComponent
      ),
    canActivate: [AuthGuard],
    data: { requiredClaims: [UserClaims.examiner] },
  },
  {
    path: 'ce-scoring/oral-examinations/exam/:examinationId',
    loadComponent: () =>
      import('./oral-examination/oral-examination.component').then(
        (m) => m.OralExaminationsComponent
      ),
    canActivate: [AuthGuard],
    data: { requiredClaims: [UserClaims.examiner] },
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
    canActivate: [AuthGuard],
    data: { requiredClaims: [UserClaims.examiner] },
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
