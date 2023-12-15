import {
  Component,
  CUSTOM_ELEMENTS_SCHEMA,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { AbsFilterType, AbsGridCellRendererType } from './abs-grid.enum';
import { isObservable } from 'rxjs';
import { DropdownModule } from 'primeng/dropdown';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { IGridColumns } from './abs-grid-col.interface';

interface GridAction {
  data: any;
  fieldKey: string;
  onClick: (data: unknown) => void;
}

@Component({
  selector: 'abs-grid',
  standalone: true,
  imports: [CommonModule, ButtonModule, InputTextModule, DropdownModule],
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.scss'],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class GridComponent implements OnInit, OnChanges {
  /**
   * Data to display in the grid
   */
  @Input() data!: any;

  /**
   * Columns to display in the grid
   * @type {IGridColumns}
   */
  @Input() columns!: IGridColumns[] | any;

  /**
   * Title to display in the grid
   * @type {string}
   */
  @Input() title!: string;

  /**
   * Whether or not to display filter
   * @type {boolean}
   */
  @Input() showFilter!: boolean;

  /**
   * Type of filter to display
   *
   */
  @Input() filterType: 'text' | 'dropdown' = 'text';

  /**
   * Placeholder for filter
   * @type {string}
   */
  @Input() filterPlaceholder = 'Search';

  /**
   * Field to filter on
   * @type {string}
   */
  @Input() filterOn = '';

  /**
   * Array of options to filter on for dropdown
   * @type {string}
   */
  @Input() filterOptions: any[] = [];

  /**
   * Message when there are no filtered results
   * @type {string}
   */
  @Input() noFilteredResultsMessage = 'There are no results to display.';

  /**
   * Whether or not to display pagination
   * @type {boolean}
   */
  @Input() pagination = false;

  /**
   * Controls current page of the grid
   * @type {number}
   */
  @Input() currentPage = 1;

  /**
   * Controls how many items to display per page
   * @type {number}
   */
  @Input() itemsPerPage = 5;

  /**
   * Whether or not to show grid lines
   * @type {boolean}
   */
  @Input() showGridLines = true;

  /**
   * Message for when there are no results to display
   * @type {string}
   */
  @Input() noResultsMessage = 'There are no results to display.';

  /**
   * Template to display when row is expanded
   * @type {any}
   */
  @Input() expandTemplate!: any;

  /**
   * Parent component action to handle
   * @type {EventEmitter<unknown>}
   */
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
        const mappedData = data?.map((item: any) => {
          return {
            ...item,
            expanded: item.expanded ?? false,
          };
        });

        this.localData = mappedData ?? [];
        this.filteredData = mappedData ?? [];
        this.initPagintion(this.localData);
      });
    }
  }

  ngOnChanges(changes: SimpleChanges) {
    if (!isObservable(this.data)) {
      if (changes['data']) {
        const mappedData = changes['data'].currentValue.map((item: any) => {
          return {
            ...item,
            expanded: item.expanded ?? false,
          };
        });
        this.localData = mappedData ?? [];
        this.filteredData = mappedData ?? [];
        this.initPagintion(this.localData);
      }
    }
  }

  /**
   *
   * Initializes pagination
   */

  initPagintion(data: any[]) {
    if (this.pagination) {
      const total = data?.length ?? 0;
      const pagesCount = Math.ceil(total / this.itemsPerPage);
      this.pages = [...Array(pagesCount).keys()].map((i) => i + 1);

      this.setPaginationActions();
    }
  }

  /**
   *
   * Sets pagination actions
   */
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

  /**
   *
   * Sets the data to display in the grid
   */
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

  /**
   *
   * Handles grid action
   */
  handleAction(action: GridAction, data: unknown) {
    action['data'] = data;
    this.action.emit(action);
  }

  /**
   *
   * Handles grid expand action
   */
  handleExpand(action: GridAction, data: any) {
    data.expanded = !data.expanded;
    action['data'] = data;
    this.action.emit(action);
  }

  /**
   *
   * Sets up column sorting functionality
   */
  setColumnSort(column: any) {
    this.columns.forEach((col: any) => {
      if (col.fieldName === column.fieldName) {
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

  /**
   *
   * Functionality for sorting columns
   */
  sortColumn(a: any, b: any) {
    const sortColumn = this.columns?.find((col: any) => col.sort);
    if (sortColumn) {
      if (sortColumn.sort === 'asc') {
        return a[sortColumn.fieldName] > b[sortColumn.fieldName] ? 1 : -1;
      } else {
        return a[sortColumn.fieldName] < b[sortColumn.fieldName] ? 1 : -1;
      }
    } else {
      return 0;
    }
  }

  /**
   *
   * Handles filter change
   */
  whenGridFilterChange($event: any) {
    const value =
      this.filterType === AbsFilterType.Text
        ? $event?.target.value
        : $event?.value?.value;

    this.filteredData = this.localData.filter((item: any) =>
      item[this.filterOn].toLowerCase().includes(value?.toLowerCase() ?? '')
    );

    if (this.pagination) {
      this.initPagintion(this.filteredData);
      this.changePage(1);
    }
  }
}
