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
  ClearOutcomeRegistriesErrors,
  ContinuousCertificationSelectors,
  CreateOutcomeRegistries,
  GetOutcomeRegistries,
  UpdateOutcomeRegistries,
} from 'src/app/state/continuous-certification';
import { UserProfileSelectors } from 'src/app/state';
import { Observable } from 'rxjs';
import { IOutcomeRegistryModel } from 'src/app/api/models/continuouscertification/outcome-registry.model';
import { OutcomeRegistriesFormFields } from './outcome-registries-form';
import { RadioButtonModule } from 'primeng/radiobutton';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { CheckboxModule } from 'primeng/checkbox';
import { ButtonModule } from 'primeng/button';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { GlobalDialogService } from 'src/app/shared/services/global-dialog.service';
import { IFormErrors } from 'src/app/shared/common';
import { FormErrorsComponent } from 'src/app/shared/components/form-errors/form-errors.component';

@UntilDestroy()
@Component({
  selector: 'abs-outcome-registries-modal',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RadioButtonModule,
    InputTextareaModule,
    CheckboxModule,
    ButtonModule,
    FormErrorsComponent,
  ],
  templateUrl: './outcome-registries-modal.component.html',
  styleUrls: ['./outcome-registries-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class OutcomeRegistriesModalComponent implements OnInit {
  //TODO: [Joe] - add form-errors shared component

  @Output() closeDialog: EventEmitter<any> = new EventEmitter();
  userId!: number;

  @Select(UserProfileSelectors.userId) userId$: Observable<number> | undefined;
  @Select(ContinuousCertificationSelectors.GetOutcomeRegistries)
  outcomeRegistries$: Observable<IOutcomeRegistryModel> | undefined;
  outcomeRegistries: IOutcomeRegistryModel | undefined;
  outcomesandRegistriesFormFields = OutcomeRegistriesFormFields;

  errors: IFormErrors | null = null;
  clearErrors = new ClearOutcomeRegistriesErrors();

  disableSubmit = true;

  outcomeRegistriesForm = new FormGroup({
    surgeonSpecificRegistry: new FormControl('', [Validators.required]),
    registryComments: new FormControl('', [Validators.required]),
    registeredWithACHQC: new FormControl(false, [Validators.required]),
    registeredWithCESQIP: new FormControl(false, [Validators.required]),
    registeredWithMBSAQIP: new FormControl(false, [Validators.required]),
    registeredWithABA: new FormControl(false, [Validators.required]),
    registeredWithASBS: new FormControl(false, [Validators.required]),
    registeredWithMSQC: new FormControl(false, [Validators.required]),
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

  constructor(
    private _store: Store,
    private _globalDialogService: GlobalDialogService
  ) {
    this._store.dispatch(new GetOutcomeRegistries());
  }

  ngOnInit(): void {
    this.getOutcomeRegistriesData();
    this.outcomeRegistriesForm.valueChanges
      .pipe(untilDestroyed(this))
      .subscribe((value) => {
        this.disableSubmit = !value.userConfirmed;
      });
  }

  getOutcomeRegistriesData() {
    this.outcomeRegistries$
      ?.pipe(untilDestroyed(this))
      .subscribe((res: any) => {
        const outcomeRegistries = res.outcomeRegistries;
        this.outcomeRegistries = outcomeRegistries;
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
    console.log(this.outcomeRegistriesForm.getRawValue());
    const formValues = {
      ...this.outcomeRegistriesForm.getRawValue(),
      userConfirmedDateUtc: new Date(),
    } as unknown as IOutcomeRegistryModel;

    console.log('OUTCOME REGISTRIES!', this.outcomeRegistries);

    if (this.outcomeRegistries) {
      this._store
        .dispatch(new UpdateOutcomeRegistries(formValues))
        .pipe(untilDestroyed(this))
        .subscribe(() => {
          this.close();
        });
    } else {
      this._store
        .dispatch(new CreateOutcomeRegistries(formValues))
        .pipe(untilDestroyed(this))
        .subscribe(() => {
          this.close();
        });
    }
  }

  close() {
    this.errors = null;
    this.closeDialog.emit();
  }
}
