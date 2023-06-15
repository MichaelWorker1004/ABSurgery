import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CheckboxModule } from 'primeng/checkbox';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'abs-attestation-modal',
  standalone: true,
  imports: [CommonModule, FormsModule, CheckboxModule, ButtonModule],
  templateUrl: './attestation-modal.component.html',
  styleUrls: ['./attestation-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AttestationModalComponent {
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();

  userData = {
    name: 'John Doe',
  };

  attestationFormFields = [
    {
      label:
        'I hereby authorize any hospital or medical staff where I now have,have had, or have applied for medical staff privileges, and anymedical organization of which I am a member or to which I have applied for membership, and any person who may have information (including medical records, patient records, and reports of committees) which is deemed by ABS to be material to its evaluation of this application, to provide such information to representatives of the ABS. I agree that communications of any nature made to the ABS regarding this application may be made in confidence and shall not be made available to me under any circumstances. I hereby release from liability any hospital. medical staff, medical organization or person, and ABS and its representatives, for acts performed in connection with this application.',
      subLabel: '',
      value: '',
      required: false,
      name: 'attestation1',
      placeholder: '',
      type: 'checkbox',
      size: 'col-12',
    },
    {
      label:
        'I understand that the certificate I will be issued upon successful completion of the biennial Continuous Certification Assessment will be contingent upon my on-going active participation in the Continuous Certification Program as a whole. I recognize that 10-year certificates are no longer offered by the ABS, and that the biennial Continuous Certification Assessment is replacing the traditional 10-vear recertification examination.',
      subLabel: '',
      value: '',
      required: false,
      name: 'attestation2',
      placeholder: '',
      type: 'checkbox',
      size: 'col-12',
    },
  ];

  close() {
    this.closeDialog.emit();
  }
}
