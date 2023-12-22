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
} from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Select } from '@ngxs/store';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { Observable } from 'rxjs';
import { IAttestationReadOnlyModel } from 'src/app/api/models/continuouscertification/attestation-read-only.model';
import { IUserProfile, UserProfileSelectors } from 'src/app/state';

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
export class AttestationModalComponent implements OnInit, OnChanges {
  @Input() source: string | undefined;
  @Input() attestations: IAttestationReadOnlyModel[] | undefined;
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();
  @Output() saveAttestation: EventEmitter<any> = new EventEmitter();

  @Select(UserProfileSelectors.user) user$:
    | Observable<IUserProfile>
    | undefined;

  attestationForm: FormGroup = new FormGroup({});

  disabledSubmit = true;

  ngOnInit(): void {
    this.setUpFormFields(this.attestations || []);

    this.attestationForm.valueChanges
      .pipe(untilDestroyed(this))
      .subscribe((values) => {
        this.disabledSubmit = !Object.values(values).every((x) => x === true);
      });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['attestations']) {
      this.setUpFormFields(changes['attestations'].currentValue);
    }
  }

  async setUpFormFields(attestations: IAttestationReadOnlyModel[]) {
    if (!attestations) return;
    const promises = attestations.map(
      (attestation: IAttestationReadOnlyModel) => {
        return new Promise<void>((resolve) => {
          this.attestationForm.addControl(
            attestation.name,
            new FormControl(attestation.checked ? true : false)
          );
          resolve();
        });
      }
    );

    await Promise.all(promises);
  }

  onSave() {
    this.saveAttestation.emit();
  }

  close() {
    this.closeDialog.emit();
  }
}
