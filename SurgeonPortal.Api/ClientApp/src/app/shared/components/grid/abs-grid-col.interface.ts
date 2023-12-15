export interface IGridColumns {
  headerName: string;
  fieldName: string;
  sortable?: boolean;
  width?: number;
  icon?: string;
  onClick?: (data: any) => void;
  appendIcon?: any;
  prependIcon?: any;
  cellRenderer?:
    | 'button'
    | 'conditionalButton'
    | 'primeButton'
    | 'yesNo'
    | 'dateFormatter'
    | 'customClass'
    | 'customStyle'
    | 'expandToggle'
    | 'fileLink';
  cellRendererParams?: any;
  buttonClass?: string;
  sort?: 'asc' | 'desc';
  fileType?: string;
  cellClass?: any;
  cellStyle?: any;
  style?: any;
}
