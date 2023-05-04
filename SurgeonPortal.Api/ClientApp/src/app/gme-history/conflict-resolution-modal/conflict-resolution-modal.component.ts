import {
  CUSTOM_ELEMENTS_SCHEMA,
  Component,
  EventEmitter,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { GridComponent } from '../../shared/components/grid/grid.component';
import { CONFLICT_RESOLUTION_GRID_COLS } from './conflict-resolution-cols';

@Component({
  selector: 'abs-conflict-resolution-modal',
  standalone: true,
  imports: [CommonModule, GridComponent],
  templateUrl: './conflict-resolution-modal.component.html',
  styleUrls: ['./conflict-resolution-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ConflictResolutionModalComponent {
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();
  conflictResolutionCols = CONFLICT_RESOLUTION_GRID_COLS;
  conflictResolutionData!: any[];

  ngOnInit() {
    this.getConflicts();
  }

  getConflicts() {
    this.conflictResolutionData = [
      {
        from: '01/01/2021',
        to: '01/07/2021',
        weeks: '1',
        programName: 'Program Name',
        affiliatedInstitute: 'Affiliated Institute',
        clinicalLevel: 'Clinical Level',
        explain: 'Explain',
        descriptionNonSurgicalOnly: 'Description (Non-Surgical Only)',
        internationalRotation: 'International Rotation',
        edit: 'Edit',
        delete: 'Delete',
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
