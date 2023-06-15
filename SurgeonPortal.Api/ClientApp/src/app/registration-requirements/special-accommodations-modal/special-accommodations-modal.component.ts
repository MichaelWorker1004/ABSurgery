import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  OnInit,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { SPECIAL_ACCOMMODATIONS_COLS } from './special-accommodations-cols';
import { GridComponent } from 'src/app/shared/components/grid/grid.component';
import { FormsModule } from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'abs-special-accommodations-modal',
  standalone: true,
  imports: [
    CommonModule,
    GridComponent,
    FormsModule,
    DropdownModule,
    ButtonModule,
  ],
  templateUrl: './special-accommodations-modal.component.html',
  styleUrls: ['./special-accommodations-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class SpecialAccommodationsModalComponent implements OnInit {
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();

  specialAccommodationsCols = SPECIAL_ACCOMMODATIONS_COLS;
  specialAccommodationsData!: any;

  fileUploadedName!: string | undefined;
  uploadedFile!: File | undefined;
  documentType!: string;

  selectedDocumentType: string | null | undefined;

  specialAccommodationsTypeOptions = [
    {
      label: 'Other medical condition',
      value: 'other_medical_condition',
    },
    {
      label: 'Lactating mother',
      value: 'lactating_mother',
    },
    {
      label: 'ADA (Learning disability)',
      value: 'ada_learning_disability',
    },
  ];

  ngOnInit(): void {
    this.getSpecialAccommodationsData();
  }

  getSpecialAccommodationsData() {
    this.specialAccommodationsData = [
      {
        fileName: 'ABC_Special-Accommodation-Request_1-2-22.pdf',
        uploadDate: new Date('09/22/19'),
        type: 'Other medical condition',
      },
    ];
  }

  handleSelectChange(event: any) {
    this.selectedDocumentType = event.target.__displayLabel;
  }

  handleFileOnChange($event: any) {
    this.fileUploadedName = $event.target.files[0].name;
    this.uploadedFile = $event.target.files;
  }

  onDocumentUpload() {
    console.log('document upload', this.uploadedFile);
    console.log('document Name', this.fileUploadedName);
    console.log('document Type', this.documentType);
    if (this.uploadedFile) {
      this.specialAccommodationsData.push({
        fileName: this.fileUploadedName,
        uploadDate: new Date(),
        type: this.selectedDocumentType,
      });
      this.resetData();
    }
  }

  resetData() {
    this.fileUploadedName = undefined;
    this.uploadedFile = undefined;
    this.selectedDocumentType = null;
  }

  close() {
    this.closeDialog.emit({ action: 'specialAccommodationsModal' });
  }

  gridAction($event: any) {
    console.log($event);
  }
}
