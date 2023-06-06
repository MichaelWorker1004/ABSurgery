import {
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { AbsFilterType, AbsGridCellRendererType } from './abs-grid.enum';
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
    filterType: AbsFilterType.Text,
    filterOptions: [],
    noFilteredResultsMessage: 'There are no results to display.',
  };
  @Input() pagination = false;
  @Input() expandTemplate!: any;
  @Input() currentPage = 1;
  @Input() itemsPerPage = 5;
  @Input() showGridLines = true;
  @Input() noResultsMessage = 'There are no results to display.';

  @Output() action: EventEmitter<unknown> = new EventEmitter();

  pages: number[] = [];
  AbsGridCellRendererType = AbsGridCellRendererType;
  searchText!: string;
  localData: Array<any> = [];
  filteredData: Array<any> = [];

  previousPageDisabled!: boolean;
  firstPageDisabled!: boolean;
  nextPageDisabled!: boolean;
  lastPageDisabled!: boolean;

  ngOnInit() {
    if (isObservable(this.data)) {
      this.data.subscribe((data: any) => {
        this.localData = data ?? [];
        this.filteredData = data ?? [];
        this.initPagintion(this.localData);
      });
    } else {
      this.localData = this.data;
      this.filteredData = this.data ?? [];
      this.initPagintion(this.data);
    }
  }

  initPagintion(data: any[]) {
    if (this.pagination) {
      const total = data?.length ?? 0;
      const pagesCount = Math.ceil(total / this.itemsPerPage);
      this.pages = [...Array(pagesCount).keys()].map((i) => i + 1);

      this.setPaginationActions();
    }
  }

  setPaginationActions() {
    this.previousPageDisabled = this.currentPage === 1;
    this.firstPageDisabled =
      this.previousPageDisabled ||
      this.pages.length <= 2 ||
      this.currentPage <= 2;

    this.nextPageDisabled = this.currentPage === this.pages.length;
    this.lastPageDisabled = this.nextPageDisabled;
  }

  changePage(page: number) {
    this.currentPage = page;

    this.setPaginationActions();
  }

  getPagedData(data: any[]) {
    let sortedData = data;
    if (data?.length > 0) {
      sortedData = [...data];
      sortedData.sort(this.sortColumn.bind(this));
    }
    if (this.pagination) {
      return sortedData.slice(
        (this.currentPage - 1) * this.itemsPerPage,
        this.currentPage * this.itemsPerPage
      );
    } else {
      return sortedData;
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

  setColumnSort(column: any) {
    this.columns.forEach((col: any) => {
      if (col.field === column.field) {
        col.sort =
          column.sort === 'asc'
            ? 'desc'
            : column.sort === 'desc'
            ? null
            : 'asc';
      } else {
        col.sort = null;
      }
    });
  }

  sortColumn(a: any, b: any) {
    const sortColumn = this.columns?.find((col: any) => col.sort);
    if (sortColumn) {
      if (sortColumn.sort === 'asc') {
        return a[sortColumn.field] > b[sortColumn.field] ? 1 : -1;
      } else {
        return a[sortColumn.field] < b[sortColumn.field] ? 1 : -1;
      }
    } else {
      return 0;
    }
  }

  onGridFilterChange($event: any) {
    const value =
      this.gridOptions.filterType === AbsFilterType.Text
        ? $event?.target.value
        : $event?.target.displayLabel;

    this.filteredData = this.localData.filter((item: any) =>
      item[this.gridOptions.filterOn]
        .toLowerCase()
        .includes(value.toLowerCase())
    );

    if (this.pagination) {
      this.initPagintion(this.filteredData);
      this.changePage(1);
    }
  }
}
