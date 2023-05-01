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
import { isObservable } from 'rxjs';

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
  localData: Array<any> = [];

  ngOnInit() {
    if (isObservable(this.data)) {
      this.data.subscribe((data: any) => {
        this.localData = data;
        this.filteredData = this.localData;
      });
    } else {
      this.localData = this.data;
      this.filteredData = this.data;
    }
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
    this.localData = this.localData.sort((a: string, b: string) => {
      if (column.sort === 'asc') {
        return a[column.field] > b[column.field] ? 1 : -1;
      } else {
        return a[column.field] < b[column.field] ? 1 : -1;
      }
    });
  }

  onGridFilterChange($event: any) {
    this.filteredData = this.localData.filter((item: any) =>
      item[this.gridOptions.filterOn]
        .toLowerCase()
        .includes($event.target.value.toLowerCase())
    );
  }
}
