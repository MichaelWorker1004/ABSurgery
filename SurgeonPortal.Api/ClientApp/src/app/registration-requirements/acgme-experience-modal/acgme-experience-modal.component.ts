import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  OnInit,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { ACGME_EXPERIENCE_GRID_COLS } from './acgme-experience-cols';
import { GridComponent } from 'src/app/shared/components/grid/grid.component';
import { ButtonModule } from 'primeng/button';
import { Select } from '@ngxs/store';
import { Observable } from 'rxjs';
import { UserProfileSelectors } from 'src/app/state';
import { DocumentsUploadComponent } from 'src/app/shared/components/documents-upload/documents-upload.component';
import { IGridColumns } from 'src/app/shared/components/grid/abs-grid-col.interface';

@Component({
  selector: 'abs-acgme-experience-modal',
  standalone: true,
  imports: [
    CommonModule,
    GridComponent,
    ButtonModule,
    DocumentsUploadComponent,
  ],
  templateUrl: './acgme-experience-modal.component.html',
  styleUrls: ['./acgme-experience-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AcgmeExperienceModalComponent implements OnInit {
  @Select(UserProfileSelectors.userId) userId$: Observable<number> | undefined;

  @Output() closeDialog: EventEmitter<any> = new EventEmitter();

  AcgmeGridCols = ACGME_EXPERIENCE_GRID_COLS as IGridColumns[];
  AcgmeGridData!: any;

  ngOnInit() {
    this.getAcgmeGridData();
  }

  getAcgmeGridData() {
    this.AcgmeGridData = [
      {
        fileName: 'ACGME-Report_11-4-22_FINAL.pdf',
        uploadDate: new Date('09/22/19'),
      },
    ];
  }

  onDocumentUpload($event: any) {
    console.log('unhandled document upload', $event);
  }

  girdAction($event: any) {
    console.log('unhandled grid action', $event);
  }

  close() {
    this.closeDialog.emit({ action: 'ACGMEExperienceModal' });
  }
}
