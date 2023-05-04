export interface IGridOptions {
  showFilter: boolean;
  filterOn: string;
  placeholder?: string;
  filterType?: string;
  filterOptions?: IDropdown[];
}

export interface IDropdown {
  value: string;
  label: string;
}
