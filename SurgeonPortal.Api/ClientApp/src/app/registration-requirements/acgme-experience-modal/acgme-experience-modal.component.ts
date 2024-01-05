import { CommonModule } from '@angular/common';
import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  Output,
} from '@angular/core';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Select, Store } from '@ngxs/store';
import { ButtonModule } from 'primeng/button';
import { Observable } from 'rxjs';
import { IUserCertificateReadOnlyModel } from 'src/app/api/models/medicaltraining/user-certificate-read-only.model';
import { DocumentsUploadComponent } from 'src/app/shared/components/documents-upload/documents-upload.component';
import { IGridColumns } from 'src/app/shared/components/grid/abs-grid-col.interface';
import { GridComponent } from 'src/app/shared/components/grid/grid.component';
import {
  DocumentSelectors,
  GetCertificateByType,
  UploadDocument,
  UserProfileSelectors,
} from 'src/app/state';
import { ACGME_EXPERIENCE_GRID_COLS } from './acgme-experience-cols';

@UntilDestroy()
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
export class AcgmeExperienceModalComponent {
  @Select(UserProfileSelectors.userId) userId$: Observable<number> | undefined;

  @Select(DocumentSelectors.slices.userCertificates) userCertificates$:
    | Observable<IUserCertificateReadOnlyModel[]>
    | undefined;

  @Output() closeDialog: EventEmitter<any> = new EventEmitter();

  AcgmeGridCols = ACGME_EXPERIENCE_GRID_COLS as IGridColumns[];

  allowUpload = true;

  constructor(private _store: Store) {
    this._store
      .dispatch(new GetCertificateByType(7))
      .pipe(untilDestroyed(this))
      .subscribe((state: any) => {
        const AcgmeUserCertificates = state.documents.userCertificates;
        if (AcgmeUserCertificates.length) {
          this.allowUpload = false;
        }
      });
  }

  onDocumentUpload($event: any) {
    const data = $event.data;
    const existingACGME = $event.gridData;
    const model = {
      certificateTypeId: 7,
      file: data.file,
      issueDate: new Date().toISOString(),
    };

    const formData = new FormData();

    Object.keys(model).forEach((key) => {
      formData.set(key, model[key as keyof typeof model]);
    });

    if (data.file) {
      if (!existingACGME.length) {
        this._store
          .dispatch(new UploadDocument({ model: formData }))
          .pipe(untilDestroyed(this))
          .subscribe(() => {
            this._store.dispatch(new GetCertificateByType(7));
            this.allowUpload = false;
            this.close();
          });
      } else {
        //update ACGME
        this.allowUpload = false;
      }
    }
  }

  girdAction($event: any) {
    console.log('unhandled grid action', $event);
  }

  close() {
    this.closeDialog.emit({ action: 'ACGMEExperienceModal' });
  }
}
