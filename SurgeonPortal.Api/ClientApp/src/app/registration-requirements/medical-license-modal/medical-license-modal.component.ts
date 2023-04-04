import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { GridComponent } from 'src/app/shared/components/grid/grid.component';
import { MEDICAL_LICENSE_GRID_COLS } from './medical-license-grid-cols';

@Component({
  selector: 'abs-medical-license-modal',
  standalone: true,
  imports: [CommonModule, GridComponent],
  templateUrl: './medical-license-modal.component.html',
  styleUrls: ['./medical-license-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class MedicalLicenseModalComponent implements OnInit {
  @Input() showDialog = false;
  @Input() modalName!: string;
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();

  medicalLicenseCols = MEDICAL_LICENSE_GRID_COLS;
  medicalLicenseData!: any;

  ngOnInit() {
    this.getMedicalLicenseData();
  }

  getMedicalLicenseData() {
    this.medicalLicenseData = [
      {
        licenseState: 'Pennsylvania',
        licenseNumber: '123456789',
        licenseType: 'MD',
        expirationDate: new Date('12/31/2021'),
        issueDate: new Date('12/31/2019'),
        varifyingOrganization: 'American Board of Surgery',
      },
    ];
  }

  handleDefaultCloseModal(event: any) {
    event.preventDefault();
  }

  close() {
    this.closeDialog.emit({ action: this.modalName });
  }
}
