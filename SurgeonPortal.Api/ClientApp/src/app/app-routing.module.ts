import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FileNotFoundComponent } from './file-not-found/file-not-found.component';

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
  },
  {
    path: 'cme-repository',
    loadComponent: () =>
      import('./cme-repository/cme-repository.component').then(
        (m) => m.CmeRepositoryComponent
      ),
  },
  {
    path: 'personal-profile',
    loadComponent: () =>
      import('./personal-profile/personal-profile.component').then(
        (m) => m.PersonalProfileComponent
      ),
  },
  {
    path: 'medical-training',
    loadComponent: () =>
      import('./medical-training/medical-training.component').then(
        (m) => m.MedicalTrainingComponent
      ),
  },
  {
    path: 'professional-standing',
    loadComponent: () =>
      import('./professional-standing/professional-standing.component').then(
        (m) => m.ProfessionalStandingComponent
      ),
  },
  {
    path: 'exam-process',
    loadComponent: () =>
      import('./exam-process/exam-process.component').then(
        (m) => m.ExamProcessComponent
      ),
  },
  {
    path: 'examination-history',
    loadComponent: () =>
      import('./examination-history/examination-history.component').then(
        (m) => m.ExaminationHistoryComponent
      ),
  },
  {
    path: 'continuous-certification',
    loadComponent: () =>
      import(
        './continuous-certification/continuous-certification.component'
      ).then((m) => m.ContinuousCertificationComponent),
  },
  {
    path: 'payment-history',
    loadComponent: () =>
      import('./payment-history/payment-history.component').then(
        (m) => m.PaymentHistoryComponent
      ),
  },
  {
    path: 'documents',
    loadComponent: () =>
      import('./documents/documents.component').then(
        (m) => m.DocumentsComponent
      ),
  },
  {
    path: 'committees',
    loadComponent: () =>
      import('./committees/committees.component').then(
        (m) => m.CommitteesComponent
      ),
  },
  {
    path: 'my-account',
    loadComponent: () =>
      import('./my-account/my-account.component').then(
        (m) => m.MyAccountComponent
      ),
  },
  {
    path: 'gme-history',
    loadComponent: () =>
      import('./gme-history/gme-history.component').then(
        (m) => m.GmeHistoryComponent
      ),
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
