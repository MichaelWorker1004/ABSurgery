import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  OnInit,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Select, Store } from '@ngxs/store';
import {
  ContinuousCertificationSelectors,
  GetOutcomeRegistries,
  UpdateOutcomeRegistries,
} from 'src/app/state/continuous-certification';
import { UserProfileSelectors } from 'src/app/state';
import { Observable } from 'rxjs';
import { IOutcomeRegistryModel } from 'src/app/api/models/continuouscertification/outcome-registry.model';
import { OutcomeRegistriesFormFields } from './outcome-registries-form';
import { SuccessFailModalComponent } from 'src/app/shared/components/success-fail-modal/success-fail-modal.component';
import { RadioButtonModule } from 'primeng/radiobutton';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { CheckboxModule } from 'primeng/checkbox';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'abs-outcome-registries-modal',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SuccessFailModalComponent,
    RadioButtonModule,
    InputTextareaModule,
    CheckboxModule,
    ButtonModule,
  ],
  templateUrl: './outcome-registries-modal.component.html',
  styleUrls: ['./outcome-registries-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class OutcomeRegistriesModalComponent implements OnInit {
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();
  userId!: number;

  @Select(UserProfileSelectors.userId) userId$: Observable<number> | undefined;
  @Select(ContinuousCertificationSelectors.GetOutcomeRegistries)
  outcomeRegistries$: Observable<IOutcomeRegistryModel> | undefined;
  outcomesandRegistriesFormFields = OutcomeRegistriesFormFields;

  call = {
    isSuccess: false,
    message: '',
    showDialog: false,
  };

  disableSubmit = true;

  outcomeRegistriesForm = new FormGroup({
    surgeonSpecificRegistry: new FormControl(false, [Validators.required]),
    registryComments: new FormControl('', [Validators.required]),
    registeredWithACHQC: new FormControl(false, [Validators.required]),
    registeredWithCESQIP: new FormControl(false, [Validators.required]),
    registeredWithMBSAQIP: new FormControl(false, [Validators.required]),
    registeredWithABA: new FormControl(false, [Validators.required]),
    registeredWithASBS: new FormControl(false, [Validators.required]),
    registeredWithStatewideCollaboratives: new FormControl(false, [
      Validators.required,
    ]),
    registeredWithABMS: new FormControl(false, [Validators.required]),
    registeredWithNCDB: new FormControl(false, [Validators.required]),
    registeredWithRQRS: new FormControl(false, [Validators.required]),
    registeredWithNSQIP: new FormControl(false, [Validators.required]),
    registeredWithNTDB: new FormControl(false, [Validators.required]),
    registeredWithSTS: new FormControl(false, [Validators.required]),
    registeredWithTQIP: new FormControl(false, [Validators.required]),
    registeredWithUNOS: new FormControl(false, [Validators.required]),
    registeredWithNCDR: new FormControl(false, [Validators.required]),
    registeredWithSVS: new FormControl(false, [Validators.required]),
    registeredWithELSO: new FormControl(false, [Validators.required]),
    userConfirmed: new FormControl(false, [Validators.requiredTrue]),
  });

  constructor(private _store: Store) {
    this._store.dispatch(new GetOutcomeRegistries());
  }

  ngOnInit(): void {
    this.getOutcomeRegistriesData();
  }

  getOutcomeRegistriesData() {
    this.outcomeRegistries$?.subscribe((res: any) => {
      const outcomeRegistries = res.outcomeRegistries;
      if (outcomeRegistries) {
        for (const [key, value] of Object.entries(outcomeRegistries)) {
          this.outcomeRegistriesForm.patchValue({
            [key]: value,
          });
        }
      }
    });
  }

  onSubmit() {
    const formValues = {
      ...this.outcomeRegistriesForm.value,
      userId: this.userId,
      userConfirmedDateUtc: new Date().toDateString(),
    };

    this._store
      .dispatch(new UpdateOutcomeRegistries(<IOutcomeRegistryModel>formValues))
      .subscribe((result: any) => {
        if (!result.continuous_certification.errors) {
          this.call = {
            showDialog: true,
            message:
              'Outcome Registries / Quality Assessment Programs Saved Successfully',
            isSuccess: true,
          };
        } else {
          this.call = {
            showDialog: true,
            message: 'An error occured while saving',
            isSuccess: false,
          };
        }
      });
  }

  close() {
    this.closeDialog.emit();
    this.call = {
      isSuccess: false,
      message: '',
      showDialog: false,
    };
  }
}
