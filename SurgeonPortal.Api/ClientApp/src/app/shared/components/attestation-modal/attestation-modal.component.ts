import { CommonModule } from '@angular/common';
import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  OnInit,
  Output,
} from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Select, Store } from '@ngxs/store';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { Observable } from 'rxjs';
import {
  ContinuousCertificationSelectors,
  GetAttestations,
  IUserProfile,
  UserProfileSelectors,
} from 'src/app/state';
import { IAttestationModel } from 'src/app/state/continuous-certification/attestation.model';

@UntilDestroy()
@Component({
  selector: 'abs-attestation-modal',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    CheckboxModule,
    ButtonModule,
  ],
  templateUrl: './attestation-modal.component.html',
  styleUrls: ['./attestation-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AttestationModalComponent implements OnInit {
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();

  @Select(UserProfileSelectors.user) user$:
    | Observable<IUserProfile>
    | undefined;

  @Select(ContinuousCertificationSelectors.slices.attestations)
  attestations$!: Observable<IAttestationModel[]> | undefined;

  attestationForm: FormGroup = new FormGroup({});

  constructor(private _store: Store) {
    this._store.dispatch(new GetAttestations());
  }

  ngOnInit(): void {
    this.attestations$?.pipe(untilDestroyed(this)).subscribe((attestations) => {
      this.setUpFormFields(attestations);
    });
  }

  async setUpFormFields(attestations: IAttestationModel[]) {
    const promises = attestations.map((attestation: IAttestationModel) => {
      return new Promise<void>((resolve) => {
        this.attestationForm.addControl(
          attestation.name,
          new FormControl(attestation.checked)
        );
        resolve();
      });
    });

    await Promise.all(promises);
  }

  onSave() {
    const formValues = this.attestationForm.getRawValue();

    console.log(formValues);
  }

  close() {
    this.closeDialog.emit();
  }
}
