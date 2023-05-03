import { CommonModule } from '@angular/common';
import { Component, CUSTOM_ELEMENTS_SCHEMA, OnInit } from '@angular/core';
import { IGridOptions } from '../shared/components/grid/grid-options.model';
import { GridComponent } from '../shared/components/grid/grid.component';
import { DOCUMENTS_COLS } from './documents-col';

@Component({
  selector: 'abs-documents',
  templateUrl: './documents.component.html',
  styleUrls: ['./documents.component.scss'],
  standalone: true,
  imports: [CommonModule, GridComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class DocumentsComponent implements OnInit {
  documentsData: any = [];
  documentsCols = DOCUMENTS_COLS;
  gridOptions: IGridOptions = {
    showFilter: true,
    filterOn: 'documentName',
  };
  fileUploadedName: string | undefined;
  uploadedFile: File | undefined;
  documentType!: string;
  selectOptions = [
    {
      label: 'Medical License',
      value: 'Medical_License',
    },
    {
      label: 'Invoice',
      value: 'Invoice',
    },
  ];

  ngOnInit() {
    this.getDocuments();
  }

  getDocuments() {
    this.documentsData = [
      {
        documentName: 'page 1',
        documentType: 'Medical License',
        uploadDate: new Date('09/12/2019'),
        uploadedBy: 'Dr. John Doe',
      },
      {
        documentName: '092119-Invoice-Reminder',
        documentType: 'Invoice',
        uploadDate: new Date('09/12/2019'),
        uploadedBy: 'ABS',
      },
      {
        documentName: 'Medical-License-2019',
        documentType: 'Medical License',
        uploadDate: new Date('09/12/2019'),
        uploadedBy: 'Dr. John Doe',
      },
      {
        documentName: 'page 2',
        documentType: 'Invoice',
        uploadDate: new Date('09/12/2019'),
        uploadedBy: 'ABS',
      },
      {
        documentName: 'Medical-License-2019',
        documentType: 'Medical License',
        uploadDate: new Date('09/12/2019'),
        uploadedBy: 'Dr. John Doe',
      },
      {
        documentName: '092119-Invoice-Reminder',
        documentType: 'Invoice',
        uploadDate: new Date('09/12/2019'),
        uploadedBy: 'ABS',
      },
      {
        documentName: 'page 3',
        documentType: 'Medical License',
        uploadDate: new Date('09/12/2019'),
        uploadedBy: 'Dr. John Doe',
      },
      {
        documentName: '092119-Invoice-Reminder',
        documentType: 'Invoice',
        uploadDate: new Date('09/12/2019'),
        uploadedBy: 'ABS',
      },
      {
        documentName: 'Medical-License-2019',
        documentType: 'Medical License',
        uploadDate: new Date('09/12/2019'),
        uploadedBy: 'Dr. John Doe',
      },
      {
        documentName: 'page 4',
        documentType: 'Invoice',
        uploadDate: new Date('09/12/2019'),
        uploadedBy: 'ABS',
      },
      {
        documentName: 'Medical-License-2019',
        documentType: 'Medical License',
        uploadDate: new Date('09/12/2019'),
        uploadedBy: 'Dr. John Doe',
      },
      {
        documentName: '092119-Invoice-Reminder',
        documentType: 'Invoice',
        uploadDate: new Date('09/12/2019'),
        uploadedBy: 'ABS',
      },
      {
        documentName: 'page 5',
        documentType: 'Medical License',
        uploadDate: new Date('09/12/2019'),
        uploadedBy: 'Dr. John Doe',
      },
      {
        documentName: '092119-Invoice-Reminder',
        documentType: 'Invoice',
        uploadDate: new Date('09/12/2019'),
        uploadedBy: 'ABS',
      },
      {
        documentName: 'Medical-License-2019',
        documentType: 'Medical License',
        uploadDate: new Date('09/12/2019'),
        uploadedBy: 'Dr. John Doe',
      },
      {
        documentName: 'page 6',
        documentType: 'Invoice',
        uploadDate: new Date('09/12/2019'),
        uploadedBy: 'ABS',
      },
    ];
  }

  handleGridAction($event: any) {
    console.log($event);
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
      this.documentsData.push({
        documentName: this.fileUploadedName,
        documentType: this.documentType,
        uploadDate: new Date(),
        uploadedBy: 'Dr. John Doe',
      });
      this.resetData();
    }
  }

  resetData() {
    this.fileUploadedName = undefined;
    this.uploadedFile = undefined;
    this.documentType = '';
  }
}
