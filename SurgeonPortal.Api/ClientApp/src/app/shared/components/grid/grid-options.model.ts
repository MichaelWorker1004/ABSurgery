export interface IGridOptions {
  showFilter: boolean;
  filterOn: string;
  filterType?: string;
  filterOptions?: IDropdown[];
  placeholder?: string;
}

export interface IDropdown {
  value: string;
  label: string;
}
