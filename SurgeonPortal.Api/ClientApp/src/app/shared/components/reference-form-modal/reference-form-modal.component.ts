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

import { InputTextModule } from 'primeng/inputtext';
import { Observable, take } from 'rxjs';
import { IStateReadOnlyModel } from 'src/app/api';
import { CollapsePanelComponent } from 'src/app/shared/components/collapse-panel/collapse-panel.component';
import { GridComponent } from 'src/app/shared/components/grid/grid.component';
import { IFormFields } from 'src/app/shared/models/form-fields/form-fields';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';
import { matchFields } from 'src/app/shared/validators/validators';
import {
  IUserProfile,
  RequestRefrence,
  UserProfileSelectors,
} from 'src/app/state';
import { IRefrenceFormReadOnlyModel } from 'src/app/state/continuous-certification/refrence-form-read-only.model';
import { IRefrenceFormModel } from 'src/app/state/continuous-certification/refrence-form.model';
import { IPickListItem, IPickListItemNumber } from 'src/app/state/picklists';
import { ADD_REFERENCE_LETTER_FIELDS } from './add-reference-letter-fields';
import { REFERENCE_FORMS_COLS } from './refrence-forms-cols';
import { InputMask, InputMaskModule } from 'primeng/inputmask';
import { IReferenceLetterModel } from 'src/app/api/models/continuouscertification/reference-letter.model';

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

  // @Select(PicklistsSelectors.slices.states) states$:
  //   | Observable<IStateReadOnlyModel[]>
  //   | undefined;

  @Select(UserProfileSelectors.user) user$:
    | Observable<IUserProfile>
    | undefined;

  @Input() picklistValues!: IReferenceLetterPicklists;
  @Input() referenceFormGridData$:
    | Observable<IRefrenceFormReadOnlyModel[] | undefined>
    | undefined;
  @Input() modalConfig: IReferenceFormModalConfig | undefined;
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();

  lapsedPath = false;
  source: string | undefined;
  formExpanded = false;
  sec_order = 0;

  referenceLetterForm = new FormGroup(
    {
      official: new FormControl('', [Validators.required]),
      roleId: new FormControl<number | null>(null, [Validators.required]),
      altRoleId: new FormControl<number | null>(null),
      explain: new FormControl(''),
      title: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required]),
      confirmEmail: new FormControl('', [Validators.required]),
      phone: new FormControl('', [Validators.required]),
      hosp: new FormControl('', [Validators.required]),
      city: new FormControl('', [Validators.required]),
      state: new FormControl('', [Validators.required]),
      fullName: new FormControl({ value: '', disabled: true }),
      confirmSend: new FormControl(false, [Validators.requiredTrue]),
    },
    {
      validators: matchFields('email', 'confirmEmail'),
    }
  );

  referenceAttestationsForm = new FormGroup({});

  referenceFormsCols = REFERENCE_FORMS_COLS;
  referenceLetterFields: IFormFields[] = ADD_REFERENCE_LETTER_FIELDS;
  fullName = '';

  constructor(
    private _store: Store,
    private globalDialogService: GlobalDialogService
  ) {}

  ngOnInit(): void {
    this.setConfigValues(this.modalConfig);
    this.setPicklists(this.picklistValues);

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

          this.referenceLetterFields.filter((field) => {
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

          this.referenceLetterFields.filter((field) => {
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
    this.referenceLetterFields.filter((field) => {
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
    }
  }

  closePanel() {
    // this.globalDialogService.closeOpenDialog();
    this.referenceLetterForm.reset();
    this.referenceLetterForm.patchValue({
      fullName: this.fullName,
    });
    this.referenceRequestPanel.collaspsePanel();
  }

  onSubmit() {
    // this function will be the attestation version of the submit
    console.log('unhandled submit');
    // const form = this.referenceLetterForm.getRawValue();

    // const model = form as unknown as IRefrenceFormModel;

    // this._store
    //   .dispatch(new RequestRefrence(model))
    //   .pipe(untilDestroyed(this))
    //   .subscribe(() => {
    //     this.referenceRequestPanel.collaspsePanel();
    //     this.globalDialogService.showSuccessError(
    //       'Reference Requested',
    //       'Your reference request has been sent successfully.',
    //       true
    //     );

    //     this.close();
    //   });
  }

  close() {
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
