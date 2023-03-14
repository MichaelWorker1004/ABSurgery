import { ICellRendererParams } from '@ag-grid-enterprise/all-modules';
// import { DateFormat } from './date-format.enum';
// import { ITableCellRoute } from './table-cell-route.model';
// import { isNullOrUndefined } from 'util';
// import { ILinkRendererParams } from './cell-renderers/link/link-renderer-params.model';
// import { IGridCellButtonListener } from './listeners/grid-cell-button-listener.model';
// import { IDateFormatterParams } from './models/date-formatter/params.model';
// import { IFormattedDateColOptions } from './models/formatted-date-col-options.model';
// import { IDatePickerParams } from './models/date-picker/params.model';
// import { IDropListColOptions } from './models/droplist-col-options.model';

export const KULTERA_GRID_EXCEL_OPTIONS = [
  {
    id: 'ExcelDateTime',
    dataType: 'dateTime',
    numberFormat: { format: 'mm/dd/yyyy hh:mm:ss;;;' },
  },
  {
    id: 'ExcelDate',
    dataType: 'dateTime',
    numberFormat: { format: 'mm/dd/yyyy;;;' },
  },
];

// tslint:disable-next-line: variable-name
const KulteraGrid = {
  /* Grid Column Assistance Functions */
  setTextDisplayCol(headerName: string, fieldName: string, options?: object) {
    let colDef = {
      headerName,
      field: fieldName,
    };

    if (options) {
      colDef = { ...colDef, ...options };
    }

    return colDef;
  },

  setTextHyperlinkCol(
    headerName: string,
    fieldName: string,
    hyperlink: string,
    replacementVariables: Array<string>,
    options?: object
  ) {
    let colDef = {
      headerName,
      field: fieldName,
      cellRenderer: (params: ICellRendererParams) => {
        if (params.data[fieldName]) {
          let link = hyperlink;

          replacementVariables.forEach((item) => {
            link = link.replace(item, params.data[item]);
          });

          return (
            `<a href="#${link}" target="_blank">` +
            params.data[fieldName] +
            '</a>'
          );
        } else {
          return null;
        }
      },
    };

    if (options) {
      colDef = { ...colDef, ...options };
    }

    return colDef;
  },

  setCustomRenderedCol(
    headerName: string,
    fieldName: string,
    cellRenderer: (params: ICellRendererParams) => string,
    options?: object
  ) {
    let colDef = {
      headerName,
      field: fieldName,
      cellRenderer,
    };

    if (options) {
      colDef = { ...colDef, ...options };
    }

    return colDef;
  },

  setTextEditableCol(
    headerName: string,
    fieldName: string,
    options?: object,
    cellStyleFnct?: (cell: any) => any
  ) {
    let colDef = {
      headerName,
      field: fieldName,
      editable: true,
      cellStyle: (params: any) => {
        if (cellStyleFnct) {
          return cellStyleFnct(params.data);
        } else {
          return null;
        }
      },
    };

    if (options) {
      colDef = { ...colDef, ...options };
    }

    return colDef;
  },

  setNumberEditableCol(
    headerName: string,
    fieldName: string,
    options?: object,
    cellStyleFnct?: (cell: any) => any
  ) {
    let colDef = {
      headerName,
      field: fieldName,
      editable: true,
      cellEditor: 'number',
      cellStyle: (params: any) => {
        if (cellStyleFnct) {
          return cellStyleFnct(params.data);
        } else {
          return null;
        }
      },
    };

    if (options) {
      colDef = { ...colDef, ...options };
    }

    return colDef;
  },

  // tslint:disable-next-line: max-line-length
  // setDropListCol(
  //     headerName: string,
  //     fieldName: string,
  //     itemDisplay: string,
  //     itemValue: string,
  //     values: Array<any>,
  //     options?: object,
  //     customOptions?: IDropListColOptions
  //   ) {
  //     let colDef = {
  //         headerName,
  //         enableRowGroup: true,
  //         editable: true,
  //         field: fieldName,
  //         filter: 'agSetColumnFilter',
  //         filterParams: {
  //             buttons: [
  //                 'apply',
  //                 'reset'
  //             ],
  //             applyMiniFilterWhileTyping: false,
  //             showTooltips: true,
  //             debounce: 200,
  //             closeOnApply: true
  //         },
  //         cellEditor: 'agRichSelectCellEditor',
  //         cellEditorParams: {
  //             values,
  //             cellRenderer: (cRparams: { value: { [x: string]: any; }; }) => {
  //                 const currentValue = cRparams.value &&
  //                     typeof cRparams.value === 'string' &&
  //                     isNullOrUndefined(cRparams.value[itemDisplay]) ? cRparams.value :
  //                     cRparams.value  && !isNullOrUndefined(cRparams.value[itemDisplay]) ? cRparams.value[itemDisplay]
  //                     : null;
  //                 return currentValue;
  //             }
  //         },
  //         valueGetter: (params: { data: { [x: string]: any; }; }) => {
  //             if (params.data && !isNullOrUndefined(values) && !isNullOrUndefined(params.data[fieldName])) {
  //                 const item: any = values.find(value => value[itemValue] === params.data[fieldName]);
  //                 if (item && item[itemDisplay]) {
  //                     return item[itemDisplay];
  //                 } else {
  //                     return null;
  //                 }
  //             }
  //             return null;
  //         },
  //         valueSetter: (params: { data: { [x: string]: any; }; newValue: { [x: string]: any; }; }) => {
  //             params.data[fieldName] = params.newValue[itemValue];
  //             if (customOptions && customOptions.onCellValueChanged) {
  //                 customOptions.onCellValueChanged(params);
  //             }
  //             return true;
  //         },
  //         getQuickFilterText: (params: { value: { [x: string]: any; }; }) => {
  //             return params.value ? params.value[itemDisplay] : null;
  //         }
  //     };

  //     // This allows a developer to pass in options for the columns that will overwrite the default values.
  //     if (options) {
  //         colDef = {...colDef, ...options};
  //     }

  //     return colDef;
  // },

  // tslint:disable-next-line: max-line-length
  setDynamicDropListCol(
    headerName: string,
    fieldName: string,
    itemDisplay: string,
    itemValue: string,
    listName: string,
    options?: object,
    callBackFunct?: (cell: any) => any
  ) {
    let colDef = {
      headerName,
      enableRowGroup: true,
      editable: true,
      field: fieldName,
      filter: 'agSetColumnFilter',
      filterParams: {
        buttons: ['apply', 'reset'],
        applyMiniFilterWhileTyping: false,
        showTooltips: true,
        debounce: 200,
        closeOnApply: true,
      },
      listName,
      cellEditor: 'agRichSelectCellEditor',
      cellEditorParams: (params: { data: { [x: string]: any } }) => {
        return {
          values: params.data[listName],
          cellRenderer: (cRparams: { value: { [x: string]: any } }) => {
            const currentValue =
              cRparams.value &&
              typeof cRparams.value === 'string' &&
              (cRparams.value[itemDisplay] === null ||
                cRparams.value[itemDisplay] === undefined)
                ? cRparams.value
                : cRparams.value &&
                  !(
                    cRparams.value[itemDisplay] === null ||
                    cRparams.value[itemDisplay] === undefined
                  )
                ? cRparams.value[itemDisplay]
                : null;
            return currentValue;
          },
        };
      },
      valueGetter: (params: { data: { [x: string]: any } }) => {
        if (
          params.data &&
          !(
            params.data[listName] === null ||
            params.data[listName] === undefined
          ) &&
          !(
            params.data[fieldName] === null ||
            params.data[fieldName] === undefined
          )
        ) {
          const item: any = params.data[listName].find(
            (value: { [x: string]: any }) =>
              value[itemValue] === params.data[fieldName]
          );
          if (item && item[itemDisplay]) {
            return item[itemDisplay];
          } else {
            return null;
          }
        }
        return null;
      },
      valueSetter: (params: {
        data: { [x: string]: any };
        newValue: { [x: string]: any };
      }) => {
        params.data[fieldName] = params.newValue[itemValue];
        if (callBackFunct) {
          callBackFunct(params);
        }
        return true;
      },
      getQuickFilterText: (params: { value: { [x: string]: any } }) => {
        return params.value ? params.value[itemDisplay] : null;
      },
    };

    // This allows a developer to pass in options for the columns that will overwrite the default values.
    if (options) {
      colDef = { ...colDef, ...options };
    }

    return colDef;
  },

  // setFormattedDateCol(
  //     headerName: string,
  //     fieldName: string,
  //     customFormat: DateFormat,
  //     excelFormat = 'ExcelDate',
  //     options?: any,
  //     customOptions?: IFormattedDateColOptions
  //   ) {
  //     let cellRendererParams: IDateFormatterParams = {
  //         customFormat
  //     };

  //     if (customOptions && customOptions.dateFormatterOptions) {
  //         const dateFormatterOptions = customOptions.dateFormatterOptions;

  //         cellRendererParams = {
  //             customFormat,
  //             customFormatValue: dateFormatterOptions.customFormatValue,
  //             isDateCleared: dateFormatterOptions.isDateCleared
  //         };
  //     }

  //     let colDef = {
  //         headerName,
  //         field: fieldName,
  //         editable: false,
  //         cellClass: excelFormat,
  //         valueGetter: (params: { data: { [x: string]: any; }; colDef: { field: string | number; }; }) => {
  //             if (isNullOrUndefined(params.data)) {
  //                 return '';
  //             }

  //             const fieldValue = params.data[params.colDef.field];
  //             // match date string with format 'm/d/y '
  //             if (/^(\d+\/\d+\/\d+\s)/.test(fieldValue)) {
  //                 const parsedDate = new Date(fieldValue);
  //                 return parsedDate.toISOString();
  //             }
  //             if (
  //                 isNullOrUndefined(fieldValue) ||
  //                 fieldValue === '' ||
  //                 (fieldValue.indexOf('/') === -1 && fieldValue.indexOf('-') === -1)
  //               ) {
  //                 return '';
  //             }

  //             const nonSlashedDate = new Date(fieldValue);
  //             return nonSlashedDate.valueOf();
  //         },
  //         cellRenderer: 'dateFormatter',
  //         cellRendererParams,
  //         cellEditor: 'datePicker',
  //         valueSetter: (_: any) => {}
  //     };

  //     if (customOptions && customOptions.datePickerOptions) {
  //         const datePickerOptions = customOptions.datePickerOptions;
  //         const cellEditorParams: IDatePickerParams = {
  //             dateCleared: datePickerOptions.dateCleared,
  //             getMaxDate: datePickerOptions.getMaxDate,
  //             getMinDate: datePickerOptions.getMinDate,
  //             isClearableDate: datePickerOptions.isClearableDate,
  //             isDateCleared: datePickerOptions.isDateCleared,
  //             isValid: datePickerOptions.isValid,
  //             unclearDate: datePickerOptions.unclearDate
  //         };

  //         colDef = {
  //             ...colDef,
  //             ...{
  //                 cellEditorParams,
  //                 cellRendererParams
  //             }
  //         };
  //     }

  //     if (options) {
  //         colDef = {...colDef, ...options};
  //     }

  //     return colDef;
  // },

  // setInternalLinkCol(
  //     headerName: string,
  //     fieldName: string,
  //     route: (value: any) => ITableCellRoute,
  //     transformValue?: (cell: any) => string,
  //     options?: object
  //   ) {
  //     let colDef = {
  //         headerName,
  //         field: fieldName,
  //         editable: false,
  //         cellRenderer: 'linkRenderer',
  //         cellRendererParams: {
  //             route,
  //             transformValue
  //         },
  //         valueGetter: (params: ILinkRendererParams) => {
  //             if (isNullOrUndefined(params.data)) {
  //                 return '';
  //             }
  //             // export the transformed value
  //             if (transformValue) {
  //                 return transformValue(params.data);
  //             }

  //             return params.data[fieldName];
  //         }
  //     };

  //     if (options) {
  //         colDef = {...colDef, ...options};
  //     }

  //     return colDef;
  // },

  // setExternalLinkCol(
  //     headerName: string,
  //     fieldName: string,
  //     externalLinkGetter: (value: any) => string,
  //     transformValue?: (cell: any) => string,
  //     openInNewTab?: boolean,
  //     options?: object
  //   ) {
  //     let colDef = {
  //         headerName,
  //         field: fieldName,
  //         editable: false,
  //         cellRenderer: 'linkRenderer',
  //         cellRendererParams: {
  //             externalLinkGetter,
  //             openInNewTab,
  //             transformValue
  //         },
  //         valueGetter: (params: ILinkRendererParams) => {
  //             if ((params.data === null || params.data === undefined)) {
  //                 return '';
  //             }
  //             // export the transformed value
  //             if (transformValue) {
  //                 return transformValue(params.data);
  //             }

  //             return params.data[fieldName];
  //         }
  //     };

  //     if (options) {
  //         colDef = {...colDef, ...options};
  //     }

  //     return colDef;
  // },

  setCheckboxCol(headerName: string, fieldName: string, options?: object) {
    let colDef = {
      headerName,
      field: fieldName,
      editable: false,
      cellRenderer: 'checkbox',
    };

    if (options) {
      colDef = { ...colDef, ...options };
    }

    return colDef;
  },

  setCustomButtonAsLinkCol(
    headerName: string,
    fieldName: string,
    onClick: (data: any) => void,
    transformValue?: (data: any) => string,
    options?: object
  ) {
    let colDef = {
      headerName,
      field: fieldName,
      editable: false,
      cellRenderer: 'linkAsButton',
      cellRendererParams: {
        fieldKey: fieldName,
        onClick,
        transformValue,
      },
    };

    if (options) {
      colDef = { ...colDef, ...options };
    }

    return colDef;
  },

  setCustomButtonCol(
    headerName: string,
    fieldName: string,
    onClick?: (data: any) => void,
    transformValue?: (data: any) => string,
    options?: object
  ) {
    let colDef = {
      headerName,
      field: fieldName,
      editable: false,
      cellRenderer: 'button',
      cellRendererParams: {
        fieldKey: fieldName,
        onClick,
        transformValue,
      },
    };

    if (options) {
      colDef = { ...colDef, ...options };
    }

    return colDef;
  },

  // setListenersOnGridColumns(listeners: IGridCellButtonListener[], gridColumns: ColDef[], eventName: string = 'onClick'): ColDef[] {
  //     listeners.forEach(listener => {
  //       const columnToBeListenedTo = gridColumns.find(col => col.field === listener.columnField);

  //       if (columnToBeListenedTo) {
  //         if (columnToBeListenedTo.cellRendererParams) {
  //             columnToBeListenedTo.cellRendererParams[eventName] = (data: any) => listener.clickListenerWithData(data);
  //         } else {
  //             const cellRendererParams = {
  //                 [eventName]: (data: any) => listener.clickListenerWithData(data)
  //             };

  //             columnToBeListenedTo.cellRendererParams = cellRendererParams;
  //         }
  //       }
  //     });

  //     return gridColumns;
  // },

  /* Context Menu */
  getDefaultContextMenuItems() {
    return ['autoSizeAll', 'excelExport', 'resetColumns'];
  },
};

export { KulteraGrid };
