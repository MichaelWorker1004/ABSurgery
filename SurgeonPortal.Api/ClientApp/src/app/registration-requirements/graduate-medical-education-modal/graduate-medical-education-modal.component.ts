import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  OnInit,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { GridComponent } from 'src/app/shared/components/grid/grid.component';
import { GRADUATE_MEDICAL_EDUCATION_GRID_COLS } from './graduate-medical-education-cols';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'abs-graduate-medical-education-modal',
  standalone: true,
  imports: [CommonModule, GridComponent, ButtonModule],
  templateUrl: './graduate-medical-education-modal.component.html',
  styleUrls: ['./graduate-medical-education-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class GraduateMedicalEducationModalComponent implements OnInit {
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();

  graduateMedicalEducationCols = GRADUATE_MEDICAL_EDUCATION_GRID_COLS;
  graduateMedicalEducationData!: any;

  ngOnInit(): void {
    this.getGraduateMedicalEducationData();
  }

  getGraduateMedicalEducationData() {
    this.graduateMedicalEducationData = [
      {
        from: new Date('09/29/15'),
        to: new Date('10/29/16'),
        weeks: 4,
        programName: 'AZ - University of Arizona [0017]',
        affiliatedInstitute: '',
        clinicalLevel: 'Clinical Level 1',
        internationalRotation: 'No',
      },
    ];
  }

  close() {
    this.closeDialog.emit({ action: 'graduateMedicalEducationModal' });
  }
}
