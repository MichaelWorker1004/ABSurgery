import { CommonModule } from '@angular/common';
import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Select, Store } from '@ngxs/store';
import { NgxMaskDirective, NgxMaskPipe, provideNgxMask } from 'ngx-mask';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { DropdownModule } from 'primeng/dropdown';

import { ActivatedRoute } from '@angular/router';
import { InputMaskModule } from 'primeng/inputmask';
import { InputTextModule } from 'primeng/inputtext';
import { Observable } from 'rxjs';
import { IStateReadOnlyModel } from 'src/app/api';
import { IReferenceLetterModel } from 'src/app/api/models/continuouscertification/reference-letter.model';
import { IPdReferenceLetterModel } from 'src/app/api/models/examinations/pd-reference-letter.model';
import { CollapsePanelComponent } from 'src/app/shared/components/collapse-panel/collapse-panel.component';
import { GridComponent } from 'src/app/shared/components/grid/grid.component';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';
import { matchFields } from 'src/app/shared/validators/validators';
import {
  CreatePdReferenceLetter,
  GetPdReferenceLetter,
  IUserProfile,
  RequestRefrence,
  UserProfileSelectors,
} from 'src/app/state';
import { IRefrenceFormReadOnlyModel } from 'src/app/state/continuous-certification/refrence-form-read-only.model';
import { IPickListItem, IPickListItemNumber } from 'src/app/state/picklists';
import { REFERENCE_FORMS_COLS } from './refrence-forms-cols';

export interface IReferenceFormModalConfig {
  lapsedPath?: boolean;
  source?: string;
}

export interface IReferenceLetterPicklists {
  stateOptions: IStateReadOnlyModel[];
  roleOptions: IPickListItemNumber[];
  altRoleOptions: IPickListItemNumber[];
  explainOptions: IPickListItem[];
}

@UntilDestroy()
@Component({
  selector: 'abs-reference-form-modal',
  standalone: true,
  imports: [
    CommonModule,
    GridComponent,
    FormsModule,
    ReactiveFormsModule,
    InputTextModule,
    DropdownModule,
    CheckboxModule,
    ButtonModule,
    CollapsePanelComponent,
    InputMaskModule,
    NgxMaskDirective,
    NgxMaskPipe,
  ],
  providers: [provideNgxMask()],
  templateUrl: './reference-form-modal.component.html',
  styleUrls: ['./reference-form-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ReferenceFormModalComponent implements OnInit, OnChanges {
  @ViewChild('referenceRequestPanel')
  referenceRequestPanel!: CollapsePanelComponent;

  @Select(UserProfileSelectors.user) user$:
    | Observable<IUserProfile>
    | undefined;

  @Input() picklistValues!: IReferenceLetterPicklists;
  @Input() referenceFormGridData$:
    | Observable<
        IRefrenceFormReadOnlyModel[] | IPdReferenceLetterModel[] | undefined
      >
    | undefined;
  @Input() referenceFormsCols = REFERENCE_FORMS_COLS;
  @Input() modalConfig: IReferenceFormModalConfig | undefined;
  @Input() formFields!: any;
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();

  examHeaderId!: number;

  lapsedPath = false;
  source: string | undefined;
  formExpanded = false;
  sec_order = 0;

  referenceLetterForm: FormGroup = new FormGroup(
    {
      confirmSend: new FormControl<boolean>({ value: false, disabled: false }, [
        Validators.required,
      ]),
    },
    { validators: matchFields('email', 'confirmEmail') }
  );
  fullName = '';

  constructor(
    private _store: Store,
    private globalDialogService: GlobalDialogService,
    private activatedRoute: ActivatedRoute
  ) {
    this.activatedRoute.params.subscribe((params) => {
      this.examHeaderId = params['examId'];
      if (this.examHeaderId) {
        this._store.dispatch(new GetPdReferenceLetter(this.examHeaderId));
      }
    });
  }

  ngOnInit(): void {
    this.setConfigValues(this.modalConfig);
    this.setPicklists(this.picklistValues);
    this.setUpFormFields();
    this.onFormChanges();
    // note to all future developers, never do this, it is stupid and hacky
    // but also it was the only thing that worked, blame shoelace
    document.addEventListener('sl-after-hide', ($event: any) => {
      if (
        $event?.srcElement?.innerHTML.includes('Reference Form') &&
        this.referenceRequestPanel
      ) {
        this.referenceRequestPanel.collaspsePanel();
      }
    });

    this.user$?.pipe(untilDestroyed(this)).subscribe((user) => {
      this.referenceLetterForm.patchValue({
        fullName: user?.fullName,
      });
      this.fullName = user?.fullName;
    });

    this.referenceFormGridData$
      ?.pipe(untilDestroyed(this))
      .subscribe((data) => {
        if (data) {
          this.sec_order = data.length + 1;
        }
      });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['modalConfig']) {
      this.setConfigValues(changes['modalConfig'].currentValue);
    }
    if (changes['picklistValues']) {
      this.setPicklists(changes['picklistValues'].currentValue);
    }
  }

  async setUpFormFields() {
    const promises = this.formFields.map((field: any) => {
      return new Promise<void>((resolve, reject) => {
        this.referenceLetterForm.addControl(
          field.name,
          new FormControl(
            { value: field.value, disabled: field.disabled ?? false },
            field.validators
          )
        );
        resolve();
      });
    });

    await Promise.all(promises);
  }

  onFormChanges() {
    this.referenceLetterForm
      .get('roleId')
      ?.valueChanges.pipe(untilDestroyed(this))
      .subscribe((val) => {
        if (val === 3) {
          //this.referenceLetterForm.get('altRoleId')?.enable();
          this.referenceLetterForm
            .get('altRoleId')
            ?.setValidators([Validators.required]);

          //this.referenceLetterForm.get('explain')?.enable();
          this.referenceLetterForm
            .get('explain')
            ?.setValidators([Validators.required]);

          this.formFields.filter((field: any) => {
            if (field.name === 'altRoleId' || field.name === 'explain') {
              field.hidden = false;
            }
          });
        } else {
          //this.referenceLetterForm.get('altRoleId')?.disable();
          this.referenceLetterForm.get('altRoleId')?.setValue(null);
          this.referenceLetterForm.get('altRoleId')?.clearValidators();

          //this.referenceLetterForm.get('explain')?.disable();
          this.referenceLetterForm.get('explain')?.setValue('');
          this.referenceLetterForm.get('explain')?.clearValidators();

          this.formFields.filter((field: any) => {
            if (field.name === 'altRoleId' || field.name === 'explain') {
              field.hidden = true;
            }
          });
        }
      });
  }

  setConfigValues(config: IReferenceFormModalConfig | undefined) {
    this.source = config?.source;
    if (config?.lapsedPath !== undefined) {
      this.lapsedPath = config.lapsedPath;
    }
  }

  setPicklists(picklistValues: IReferenceLetterPicklists) {
    this.formFields.filter((field: any) => {
      if (field.name === 'state') {
        field.options = picklistValues.stateOptions;
      }
      if (field.name === 'roleId') {
        field.options = picklistValues.roleOptions;
      }
      if (field.name === 'altRoleId') {
        field.options = picklistValues.altRoleOptions;
      }
      if (field.name === 'explain') {
        field.options = picklistValues.explainOptions;
      }
    });
  }

  handleGridAction(event: any) {
    if (event.fieldKey !== 'view') {
      console.log('unhandled action', event);
    }
  }

  onSubmitPanel() {
    this.globalDialogService.showLoading();
    const form = this.referenceLetterForm.getRawValue();

    const model = {
      ...form,
      secOrder: this.source === 'continuousCertification' ? this.sec_order : 10,
    } as unknown as IReferenceLetterModel;

    if (this.source === 'continuousCertification') {
      this._store
        .dispatch(new RequestRefrence(model))
        .pipe(untilDestroyed(this))
        .subscribe((response) => {
          if (response.continuous_certification.referenceLetterErrors) {
            this.globalDialogService.showSuccessError(
              'Reference Request Failed',
              'There was an error processing your reference request. Please review the form and try again.',
              false
            );
          } else {
            this.globalDialogService.showSuccessError(
              'Reference Requested',
              'Your reference request has been sent successfully.',
              true
            );

            this.closePanel();
          }
        });
    } else {
      this.globalDialogService.closeOpenDialog();
      this.onSubmit();
    }
  }

  closePanel() {
    this.referenceLetterForm.reset();
    this.referenceLetterForm.patchValue({
      fullName: this.fullName,
    });
    this.referenceRequestPanel.collaspsePanel();
  }

  onSubmit() {
    const form = this.referenceLetterForm.getRawValue();
    const model = {
      hosp: form.hosp,
      official: form.official,
      title: form.title,
      email: form.email,
      examId: this.examHeaderId,
    } as unknown as IPdReferenceLetterModel;

    this._store
      .dispatch(new CreatePdReferenceLetter(model))
      .pipe(untilDestroyed(this))
      .subscribe(() => {
        this._store.dispatch(new GetPdReferenceLetter(this.examHeaderId));
      });

    this.close();
  }

  close() {
    this.referenceLetterForm.reset();
    this.closeDialog.emit();
  }

  togglePanelHandler(event: any) {
    this.formExpanded = event.expanded;
    // if (event.expanded) {
    //   // handle form reset as part of expand
    //   //console.log('panel expanded', event);
    // } else {
    //   // handle form cancel as part of collapse
    //   //console.log('panel collapsed', event);
    // }
  }
}
