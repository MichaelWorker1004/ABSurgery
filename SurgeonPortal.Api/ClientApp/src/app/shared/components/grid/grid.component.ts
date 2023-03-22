import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AbsGridCellRendererType } from './abs-grid.enum';

interface GridAction {
  data: any;
  fieldKey: string;
  onClick: (data: unknown) => void;
}

@Component({
  selector: 'abs-grid',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.scss'],
})
export class GridComponent {
  @Input() data!: any;
  @Input() columns!: any;
  @Input() actions!: any;
  @Input() title!: string;
  @Input() subTitle!: string;
  @Output() action: EventEmitter<any> = new EventEmitter();
  AbsGridCellRendererType = AbsGridCellRendererType;

  handleAction(action: GridAction, data: unknown) {
    action['data'] = data;
    this.action.emit(action);
  }

  sortColumn(column: any) {
    this.columns.forEach((col: any) => {
      if (col.fieldKey !== column.fieldKey) {
        col.sort = null;
      }
    });
    column.sort = column.sort === 'asc' ? 'desc' : 'asc';
    this.data = this.data.sort((a: string, b: string) => {
      if (column.sort === 'asc') {
        return a[column.field] > b[column.field] ? 1 : -1;
      } else {
        return a[column.field] < b[column.field] ? 1 : -1;
      }
    });
  }
}
