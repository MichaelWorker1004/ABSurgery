import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FileNotFoundComponent } from './file-not-found/file-not-found.component';
import { AuthGuard } from './state/auth/auth.guard';

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
    // canActivate: [AuthGuard],
  },
  {
    path: 'cme-repository',
    loadComponent: () =>
      import('./cme-repository/cme-repository.component').then(
        (m) => m.CmeRepositoryComponent
      ),
    canActivate: [AuthGuard],
  },
  {
    path: 'personal-profile',
    loadComponent: () =>
      import('./personal-profile/personal-profile.component').then(
        (m) => m.PersonalProfileComponent
      ),
    canActivate: [AuthGuard],
  },
  {
    path: 'medical-training',
    loadComponent: () =>
      import('./medical-training/medical-training.component').then(
        (m) => m.MedicalTrainingComponent
      ),
    canActivate: [AuthGuard],
  },
  {
    path: 'professional-standing',
    loadComponent: () =>
      import('./professional-standing/professional-standing.component').then(
        (m) => m.ProfessionalStandingComponent
      ),
    canActivate: [AuthGuard],
  },
  {
    path: 'exam-process',
    loadComponent: () =>
      import('./exam-process/exam-process.component').then(
        (m) => m.ExamProcessComponent
      ),
    canActivate: [AuthGuard],
  },
  {
    path: 'examination-history',
    loadComponent: () =>
      import('./examination-history/examination-history.component').then(
        (m) => m.ExaminationHistoryComponent
      ),
    canActivate: [AuthGuard],
  },
  {
    path: 'continuous-certification',
    loadComponent: () =>
      import(
        './continuous-certification/continuous-certification.component'
      ).then((m) => m.ContinuousCertificationComponent),
    // canActivate: [AuthGuard],
  },
  {
    path: 'payment-history',
    loadComponent: () =>
      import('./payment-history/payment-history.component').then(
        (m) => m.PaymentHistoryComponent
      ),
    canActivate: [AuthGuard],
  },
  {
    path: 'documents',
    loadComponent: () =>
      import('./documents/documents.component').then(
        (m) => m.DocumentsComponent
      ),
    canActivate: [AuthGuard],
  },
  {
    path: 'committees',
    loadComponent: () =>
      import('./committees/committees.component').then(
        (m) => m.CommitteesComponent
      ),
    canActivate: [AuthGuard],
  },
  {
    path: 'my-account',
    loadComponent: () =>
      import('./my-account/my-account.component').then(
        (m) => m.MyAccountComponent
      ),
    canActivate: [AuthGuard],
  },
  {
    path: 'gme-history',
    loadComponent: () =>
      import('./gme-history/gme-history.component').then(
        (m) => m.GmeHistoryComponent
      ),
    canActivate: [AuthGuard],
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
