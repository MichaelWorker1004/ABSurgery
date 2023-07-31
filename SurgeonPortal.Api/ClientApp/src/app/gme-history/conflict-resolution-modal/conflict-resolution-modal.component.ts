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
import { CommonModule } from '@angular/common';
import { GridComponent } from '../../shared/components/grid/grid.component';
import { CONFLICT_RESOLUTION_GRID_COLS } from './conflict-resolution-cols';
import { ITEMIZED_GME_COLS } from '../itemized-gme-cols';
import { ButtonModule } from 'primeng/button';
import { IRotationGapReadOnlyModel, IRotationReadOnlyModel } from 'src/app/api';

@Component({
  selector: 'abs-conflict-resolution-modal',
  standalone: true,
  imports: [CommonModule, GridComponent, ButtonModule],
  templateUrl: './conflict-resolution-modal.component.html',
  styleUrls: ['./conflict-resolution-modal.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class ConflictResolutionModalComponent implements OnChanges {
  @Output() closeDialog: EventEmitter<any> = new EventEmitter();
  @Output() editRecord: EventEmitter<any> = new EventEmitter();
  @Output() addRecord: EventEmitter<any> = new EventEmitter();
  @Input() conflictingRecords: IRotationReadOnlyModel[] = [];
  @Input() gapData: IRotationGapReadOnlyModel | undefined;
  conflictResolutionCols = CONFLICT_RESOLUTION_GRID_COLS;
  localConflictsData: IRotationReadOnlyModel[] = [];

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['conflictingRecords']) {
      this.localConflictsData = changes['conflictingRecords'].currentValue;
    }
  }

  girdAction($event: any) {
    if ($event.fieldKey === 'edit') {
      this.editRecord.emit($event.data.id);
    } else {
      console.log('unhandled action', $event);
    }
  }

  addNewRecord() {
    const params = {
      startDate: this.gapData?.startDate,
      endDate: this.gapData?.endDate,
    };
    this.addRecord.emit(params);
  }

  close() {
    this.closeDialog.emit({ action: 'ACGMEExperienceModal' });
  }
}
