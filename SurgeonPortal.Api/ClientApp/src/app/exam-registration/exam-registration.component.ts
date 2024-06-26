import { CommonModule } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA, Component, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CollapsePanelComponent } from '../shared/components/collapse-panel/collapse-panel.component';
import { PayFeeComponent } from '../shared/components/pay-fee/pay-fee.component';

import { ActivatedRoute, Router } from '@angular/router';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Select, Store } from '@ngxs/store';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { RadioButtonModule } from 'primeng/radiobutton';
import { Observable } from 'rxjs';
import { IApplicationFeeReadOnlyModel } from '../api/models/billing/application-fee-read-only.model';
import { IExamIntentionsModel } from '../api/models/examinations/exam-intentions.model';
import { GlobalDialogService } from '../shared/services/global-dialog.service';
import {
  AuthSelectors,
  CompleteExamRegistration,
  ExamProcessSelectors,
  GetAdmissionCardAvailability,
  GetExamFeeByExamId,
  GetExamIntentions,
  GetSiteSelection,
  ReqistrationRequirmentsSelectors,
  SetSiteSelection,
  UpdateExamIntentions,
} from '../state';
import {
  GetSiteSelctionList,
  IPickListItem,
  PicklistsSelectors,
} from '../state/picklists';
import { IExamTitleReadOnlyModel } from '../api/models/examinations/exam-title-read-only.model';
import { IAdmissionCardAvailabilityReadOnlyModel } from '../api/models/examinations/admission-card-availability-read-only.model';
import { ReportService } from '../api/services/reports/reports.service';

@UntilDestroy()
@Component({
  selector: 'abs-exam-registration',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    CollapsePanelComponent,
    PayFeeComponent,
    RadioButtonModule,
    CheckboxModule,
    ButtonModule,
  ],
  templateUrl: './exam-registration.component.html',
  styleUrls: ['./exam-registration.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ExamRegistrationComponent implements OnInit {
  @Select(ReqistrationRequirmentsSelectors.slices.examTitle) examTitle$:
    | Observable<IExamTitleReadOnlyModel>
    | undefined;

  @Select(PicklistsSelectors.slices.siteSelectionPicklist)
  siteSelectionPicklist$: Observable<IPickListItem[]> | undefined;

  @Select(ExamProcessSelectors.slices.examFeeByExamId) examFeeByExamId$:
    | Observable<IApplicationFeeReadOnlyModel[]>
    | undefined;

  @Select(ExamProcessSelectors.slices.siteSelection) siteSelection$:
    | Observable<string>
    | undefined;

  @Select(ExamProcessSelectors.slices.examIntentions) examIntentions$:
    | Observable<IExamIntentionsModel>
    | undefined;

  @Select(ExamProcessSelectors.slices.admissionCardAvailability)
  admissionCardAvailability$:
    | Observable<IAdmissionCardAvailabilityReadOnlyModel>
    | undefined;

  examRegistrationFormData = new FormGroup({
    siteSelection: new FormControl('', Validators.required),
    examIntention: new FormControl(false, Validators.required),
  });

  siteSelectionPicklist!: any[];
  examId!: number;
  access_token = '';

  payFeeData: any;

  completeExamRegistrationEnabled = false;

  constructor(
    private _store: Store,
    private route: ActivatedRoute,
    private router: Router,
    private globalDialogService: GlobalDialogService,
    private reportService: ReportService
  ) {
    this.route.params.pipe(untilDestroyed(this)).subscribe((params) => {
      const examHeaderId = params['examId'];
      if (examHeaderId) {
        this.examId = examHeaderId;
        this._store.dispatch(new GetSiteSelctionList(examHeaderId));
        this._store.dispatch(new GetExamFeeByExamId(examHeaderId));
        this._store.dispatch(new GetSiteSelection(examHeaderId));
        this._store.dispatch(new GetExamIntentions(examHeaderId));
        this._store.dispatch(new GetAdmissionCardAvailability(examHeaderId));
        this.getPayFeeData();
      }
    });

    this.access_token =
      this._store.selectSnapshot(AuthSelectors.slices.access_token) || '';
  }

  ngOnInit(): void {
    this.populateFormData();
  }

  checkIfCompleteExamRegistration() {
    const examIntentions =
      this.examRegistrationFormData?.get('examIntention')?.value;

    this.admissionCardAvailability$
      ?.pipe(untilDestroyed(this))
      .subscribe(
        (
          admissionCardAvailability: IAdmissionCardAvailabilityReadOnlyModel
        ) => {
          if (
            examIntentions === true &&
            this.payFeeData.remainingBalance === 0 &&
            admissionCardAvailability?.admCardAvailable === true
          ) {
            console.log('exam not ready');
            this.completeExamRegistrationEnabled = true;
          }
        }
      );
  }

  populateFormData() {
    this.siteSelection$
      ?.pipe(untilDestroyed(this))
      .subscribe((siteSelection: string) => {
        if (siteSelection) {
          this.examRegistrationFormData.patchValue({
            siteSelection: siteSelection,
          });
        }
      });

    this.examIntentions$?.pipe(untilDestroyed(this)).subscribe((intentions) => {
      if (intentions?.intention === true) {
        this.examRegistrationFormData.patchValue({
          examIntention: intentions.intention,
        });
        this.examRegistrationFormData.controls['examIntention'].disable();
        this.checkIfCompleteExamRegistration();
      }
    });
  }

  getPayFeeData() {
    this.examFeeByExamId$?.subscribe((examFees) => {
      const payFeeData = {
        totalAmountOfFee: 0,
        totalAmountPaidDate: new Date(),
        totalAmountPaid: 0,
        remainingBalance: 0,
      };

      examFees.forEach((examFee: IApplicationFeeReadOnlyModel) => {
        payFeeData.totalAmountOfFee += examFee.subTotal;
        payFeeData.totalAmountPaid += examFee.paidTotal;
        payFeeData.remainingBalance += examFee.balanceDue;
      });

      this.payFeeData = payFeeData;
      this.checkIfCompleteExamRegistration();
    });
  }

  handleSiteSelectionSubmit() {
    const siteSelection =
      this.examRegistrationFormData.getRawValue().siteSelection;

    if (siteSelection) {
      this._store.dispatch(new SetSiteSelection(siteSelection));
    }
  }

  handleSubmitExamIntention() {
    const intention = this.examRegistrationFormData.getRawValue().examIntention;
    const model = {
      examId: this.examId,
      dateReceived: new Date().toISOString(),
      intention,
    } as IExamIntentionsModel;

    this.globalDialogService.showLoading();

    this._store
      .dispatch(new UpdateExamIntentions(model))
      ?.pipe(untilDestroyed(this))
      .subscribe(() => {
        this.globalDialogService.closeOpenDialog();
      });
  }

  handleDownloadForm(examCode: string, type: string) {
    this.reportService.downloadAdmissionCard(examCode, type, this.access_token);
  }

  completeExamRegistration() {
    this.globalDialogService.showLoading();
    this._store
      .dispatch(new CompleteExamRegistration(this.examId))
      .pipe(untilDestroyed(this))
      .subscribe((res) => {
        const errors = res?.registration_requirements?.errors;
        if (!errors) {
          this.globalDialogService
            .showSuccessError('Success', 'Exam registration complete', true)
            .then(() => {
              this.router.navigate(['/apply-and-register']);
            });
        } else {
          this.globalDialogService.showSuccessError(
            'Error',
            'There was an error completing your exam registration',
            false
          );
        }
      });
  }
}
