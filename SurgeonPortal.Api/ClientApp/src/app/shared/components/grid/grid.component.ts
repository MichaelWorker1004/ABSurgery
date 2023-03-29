import {
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { AbsGridCellRendererType } from './abs-grid.enum';
import { IGridOptions } from './grid-options.model';

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
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class GridComponent implements OnInit {
  @Input() data!: any;
  @Input() columns!: any;
  @Input() actions!: any;
  @Input() title!: string;
  @Input() subTitle!: string;
  @Input() gridOptions: IGridOptions = {
    showFilter: false,
    filterOn: '',
  };
  @Input() expandTemplate!: any;
  @Output() action: EventEmitter<any> = new EventEmitter();
  AbsGridCellRendererType = AbsGridCellRendererType;

  searchText!: string;
  filteredData: Array<any> = [];

  ngOnInit() {
    this.filteredData = this.data;
  }

  handleAction(action: GridAction, data: unknown) {
    action['data'] = data;
    this.action.emit(action);
  }

  handleExpand(action: GridAction, data: any) {
    data.expanded = !data.expanded;
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

  onGridFilterChange($event: any) {
    this.filteredData = this.data.filter((item: any) =>
      item[this.gridOptions.filterOn]
        .toLowerCase()
        .includes($event.target.value.toLowerCase())
    );
  }
}
