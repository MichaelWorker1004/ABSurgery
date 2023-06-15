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

@Component({
  selector: 'abs-acgme-experience-modal',
  standalone: true,
  imports: [CommonModule, GridComponent, ButtonModule],
  templateUrl: './acgme-experience-modal.component.html',
  styleUrls: ['./acgme-experience-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AcgmeExperienceModalComponent implements OnInit {
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();

  AcgmeGridCols = ACGME_EXPERIENCE_GRID_COLS;
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

  girdAction($event: any) {
    console.log($event);
  }

  close() {
    this.closeDialog.emit({ action: 'ACGMEExperienceModal' });
  }
}
