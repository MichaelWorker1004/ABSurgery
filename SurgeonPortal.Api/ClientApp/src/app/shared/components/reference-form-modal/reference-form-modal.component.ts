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
import { Observable } from 'rxjs';
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
import { PicklistsSelectors } from 'src/app/state/picklists';
import { ADD_REFERENCE_LETTER_FIELDS } from './add-reference-letter-fields';
import { REFERENCE_FORMS_COLS } from './refrence-forms-cols';
import { InputMask, InputMaskModule } from 'primeng/inputmask';

export interface IReferenceFormModalConfig {
  lapsedPath?: boolean;
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

  @Select(PicklistsSelectors.slices.states) states$:
    | Observable<IStateReadOnlyModel[]>
    | undefined;

  @Select(UserProfileSelectors.user) user$:
    | Observable<IUserProfile>
    | undefined;

  @Input() referenceFormGridData$:
    | Observable<IRefrenceFormReadOnlyModel[] | undefined>
    | undefined;
  @Input() modalConfig: IReferenceFormModalConfig | undefined;
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();

  lapsedPath = false;
  formExpanded = false;

  referenceLetterForm = new FormGroup(
    {
      nameOfAuthenticatingOfficial: new FormControl('', [Validators.required]),
      authenticatingOfficialRole: new FormControl('', [Validators.required]),
      reasonForAlternateOfficial: new FormControl(''),
      authenticatingOfficialTitle: new FormControl(''),
      authenticatingOfficialEmail: new FormControl('', [Validators.required]),
      confirmEmailAddress: new FormControl('', [Validators.required]),
      authenticatingOfficialPhoneNumber: new FormControl('', [
        Validators.required,
      ]),
      nameOfAffiliatedInstitution: new FormControl(''),
      city: new FormControl('', [Validators.required]),
      states: new FormControl('', [Validators.required]),
      name: new FormControl({ value: '', disabled: true }),
    },
    {
      validators: matchFields(
        'authenticatingOfficialEmail',
        'confirmEmailAddress'
      ),
    }
  );

  referenceAttestationsForm = new FormGroup({});

  referenceFormsCols = REFERENCE_FORMS_COLS;
  referenceLetterFields: IFormFields[] = ADD_REFERENCE_LETTER_FIELDS;

  constructor(
    private _store: Store,
    private globalDialogService: GlobalDialogService
  ) {}

  ngOnInit(): void {
    this.setConfigValues(this.modalConfig);

    this.setPicklists();
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
        name: user?.fullName,
      });
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['modalConfig']) {
      this.setConfigValues(this.modalConfig);
    }
  }

  setConfigValues(config: IReferenceFormModalConfig | undefined) {
    if (config?.lapsedPath !== undefined) {
      this.lapsedPath = config.lapsedPath;
    }
  }

  setPicklists() {
    this.states$?.pipe(untilDestroyed(this)).subscribe((states) => {
      this.referenceLetterFields.filter((field) => {
        if (field.name === 'states') {
          field.options = states;
        }
      });
    });
  }

  handleGridAction(event: any) {
    if (event.fieldKey === 'view') {
      console.log('view', event);
    } else if (event.fieldKey === 'delete') {
      console.log('delete', event);
    } else {
      console.log('unhandled action', event);
    }
  }

  onSubmitPanel() {
    const form = this.referenceLetterForm.getRawValue();

    const model = form as unknown as IRefrenceFormModel;

    this._store
      .dispatch(new RequestRefrence(model))
      .pipe(untilDestroyed(this))
      .subscribe(() => {
        this.globalDialogService.showSuccessError(
          'Reference Requested',
          'Your reference request has been sent successfully.',
          true
        );

        this.closePanel();
      });
  }

  closePanel() {
    console.log('here');
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
    if (event.expanded) {
      // handle form reset as part of expand
      console.log('panel expanded', event);
    } else {
      // handle form cancel as part of collapse
      console.log('panel collapsed', event);
    }
  }
}