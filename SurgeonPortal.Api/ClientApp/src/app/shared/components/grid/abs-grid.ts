import { IGridColumns } from './abs-grid-col.interface';

const AbsGrid = {
  setTextDisplayCol(
    headerName: string,
    fieldName: string,
    sortable?: boolean,
    width?: number
  ) {
    const colDef: IGridColumns = {
      headerName: headerName,
      fieldName,
      sortable,
      width,
    } as IGridColumns;

    if (!('appendIcon' in colDef)) {
      colDef.appendIcon = (icon?: string) => {
        if (icon) {
          colDef.appendIcon = icon;
        }
        return colDef;
      };
    }

    if (!('prependIcon' in colDef)) {
      colDef.prependIcon = (icon?: string) => {
        if (icon) {
          colDef.prependIcon = icon;
        }
        return colDef;
      };
    }

    return colDef;
  },

  setCurrencyDisplayCol(
    headerName: string,
    fieldName: string,
    sortable?: boolean,
    width?: number
  ) {
    const colDef = {
      headerName: headerName,
      field: fieldName,
      cellRenderer: 'currency',
      sortable,
      width,
    };

    return colDef;
  },

  setYesNoDisplayCol(
    headerName: string,
    fieldName: string,
    sortable?: boolean,
    width?: number
  ) {
    const colDef = {
      headerName: headerName,
      fieldName,
      cellRenderer: 'yesNo',
      sortable,
      width,
    };

    return colDef;
  },

  setCustomButtonCol(
    headerName: string,
    fieldName: string,
    icon?: string,
    onClick?: (data: any) => void,
    sortable?: boolean,
    width?: number
  ): IGridColumns {
    const colDef: IGridColumns = {
      headerName: headerName,
      fieldName,
      icon: icon,
      cellRenderer: 'button',
      cellRendererParams: {
        fieldKey: headerName.toLowerCase(),
        onClick,
      },
      sortable,
      width,
    } as IGridColumns;

    return colDef;
  },

  setCustomButtonConditionalCol(
    headerName: string,
    fieldName: string,
    icon?: string,
    onClick?: (data: any) => void,
    sortable?: boolean,
    width?: number
  ) {
    const colDef = {
      headerName: headerName,
      fieldName,
      icon: icon,
      cellRenderer: 'conditionalButton',
      cellRendererParams: {
        fieldKey: fieldName,
        onClick,
      },
      sortable,
      width,
    };

    return colDef;
  },

  setCustomPrimeButtonCol(
    headerName: string,
    fieldName: string,
    icon?: string,
    buttonClass?: string,
    onClick?: (data: any) => void,
    sortable?: boolean,
    width?: number
  ) {
    const colDef = {
      headerName: headerName,
      fieldName,
      icon: icon,
      buttonClass: buttonClass,
      cellRenderer: 'primeButton',
      cellRendererParams: {
        fieldKey: fieldName,
        onClick,
      },
      sortable,
      width,
    };

    return colDef;
  },

  setFileLinkCol(
    headerName: string,
    fieldName: string,
    fileType?: string,
    onClick?: (data: any) => void,
    sortable?: boolean,
    width?: number
  ) {
    const colDef = {
      headerName: headerName,
      fieldName,
      fileType: fileType,
      cellRenderer: 'fileLink',
      cellRendererParams: {
        fieldKey: fieldName,
        onClick,
      },
      sortable,
      width,
    };

    return colDef;
  },

  setExpandToggle(
    headerName: string,
    fieldName: string,
    onClick?: (data: any) => void,
    sortable?: boolean,
    width?: number
  ) {
    const colDef = {
      headerName: headerName,
      fieldName,
      cellRenderer: 'expandToggle',
      cellRendererParams: {
        fieldKey: fieldName,
        onClick,
      },
      sortable,
      width,
    };

    return colDef;
  },

  setFormattedDateCol(
    headerName: string,
    fieldName: string,
    sortable?: boolean,
    width?: number
  ) {
    const colDef = {
      headerName,
      fieldName,
      cellRenderer: 'dateFormatter',
      sortable,
      width,
    };
    return colDef;
  },

  setCellCustomClass(
    headerName: string,
    fieldName: string,
    classField?: string,
    sortable?: boolean,
    className?: string,
    width?: number
  ): IGridColumns {
    const colDef = {
      headerName,
      fieldName,
      classField,
      cellRenderer: 'customClass',
      cellClass: (fieldName: string) => {
        return className ?? fieldName;
      },
      sortable,
      width,
    } as IGridColumns;

    if (!('appendIcon' in colDef)) {
      colDef.appendIcon = (icon?: string) => {
        if (icon) {
          colDef.appendIcon = icon;
        }
        return colDef;
      };
    }

    if (!('prependIcon' in colDef)) {
      colDef.prependIcon = (icon?: string) => {
        if (icon) {
          colDef.prependIcon = icon;
        }
        return colDef;
      };
    }

    return colDef;
  },

  setCellCustomStyle(
    headerName: string,
    fieldName: string,
    style?: object,
    sortable?: boolean,
    width?: number
  ) {
    const colDef = {
      headerName,
      fieldName,
      cellRenderer: 'customStyle',
      style: () => {
        return style ?? {};
      },
      sortable,
      width,
    } as IGridColumns;

    if (!('appendIcon' in colDef)) {
      colDef.appendIcon = (icon?: string) => {
        if (icon) {
          colDef.appendIcon = icon;
        }
        return colDef;
      };
    }

    if (!('prependIcon' in colDef)) {
      colDef.prependIcon = (icon?: string) => {
        if (icon) {
          colDef.prependIcon = icon;
        }
        return colDef;
      };
    }

    return colDef;
  },
};

export { AbsGrid };
