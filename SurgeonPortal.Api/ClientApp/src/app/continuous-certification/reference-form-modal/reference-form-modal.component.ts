import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  OnInit,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { GridComponent } from 'src/app/shared/components/grid/grid.component';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { REFERENCE_FORMS_COLS } from './refrence-forms-cols';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { CheckboxModule } from 'primeng/checkbox';
import { IFormFields } from 'src/app/shared/models/form-fields/form-fields';
import { Select, Store } from '@ngxs/store';
import { PicklistsSelectors } from 'src/app/state/picklists';
import { Observable } from 'rxjs';
import { IStateReadOnlyModel } from 'src/app/api';
import { ButtonModule } from 'primeng/button';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { ADD_REFERENCE_LETTER_FIELDS } from './add-reference-letter-fields';
import { CollapsePanelComponent } from 'src/app/shared/components/collapse-panel/collapse-panel.component';
import {
  ContinuousCertificationSelectors,
  IUserProfile,
  RequestRefrence,
  UserProfileSelectors,
} from 'src/app/state';
import { IRefrenceFormReadOnlyModel } from 'src/app/state/continuous-certification/refrence-form-read-only.model';
import { IRefrenceFormModel } from 'src/app/state/continuous-certification/refrence-form.model';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';

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
  ],
  templateUrl: './reference-form-modal.component.html',
  styleUrls: ['./reference-form-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ReferenceFormModalComponent implements OnInit {
  @Select(PicklistsSelectors.slices.states) states$:
    | Observable<IStateReadOnlyModel[]>
    | undefined;

  @Select(ContinuousCertificationSelectors.slices.refrenceFormGridData)
  refrenceFormGridData$:
    | Observable<IRefrenceFormReadOnlyModel[] | undefined>
    | undefined;

  @Select(UserProfileSelectors.user) user$:
    | Observable<IUserProfile>
    | undefined;

  @Output() closeDialog: EventEmitter<any> = new EventEmitter();

  lapsedPath = true; // TODO - will be set from user data
  formExpanded = false;

  referenceLetterForm = new FormGroup({
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
  });

  referenceAttestationsForm = new FormGroup({});

  referenceFormsCols = REFERENCE_FORMS_COLS;
  referenceLetterFields: IFormFields[] = ADD_REFERENCE_LETTER_FIELDS;

  constructor(
    private _store: Store,
    private globalDialogService: GlobalDialogService
  ) {}

  ngOnInit(): void {
    this.setPicklists();

    this.user$?.pipe(untilDestroyed(this)).subscribe((user) => {
      this.referenceLetterForm.patchValue({
        name: user?.fullName,
      });
    });
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
    console.log('unhandled action', event);
  }

  onSubmit() {
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

        this.close();
      });
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
