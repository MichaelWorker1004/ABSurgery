const AbsGrid = {
  setTextDisplayCol(headerName: string, fieldName: string, sortable?: boolean) {
    const colDef = {
      headerName: headerName,
      field: fieldName,
      sortable,
    };

    return colDef;
  },

  setCustomButtonCol(
    headerName: string,
    fieldName: string,
    icon?: string,
    onClick?: (data: any) => void,
    sortable?: boolean
  ) {
    const colDef = {
      headerName: headerName,
      field: fieldName,
      icon: icon,
      cellRenderer: 'button',
      cellRendererParams: {
        fieldKey: fieldName,
        onClick,
      },
      sortable,
    };

    return colDef;
  },

  setFormattedDateCol(
    headerName: string,
    fieldName: string,
    sortable?: boolean
  ) {
    const colDef = {
      headerName,
      field: fieldName,
      cellRenderer: 'dateFormatter',
      sortable,
    };
    return colDef;
  },

  setCellCustomClass(
    headerName: string,
    fieldName: string,
    className?: string,
    sortable?: boolean
  ) {
    const colDef = {
      headerName,
      field: fieldName,
      cellRenderer: 'customClass',
      cellClass: (fieldName: string) => {
        return className ?? fieldName;
      },
      sortable,
    };
    return colDef;
  },

  setCellCustomStyle(
    headerName: string,
    fieldName: string,
    style?: object,
    sortable?: boolean
  ) {
    const colDef = {
      headerName,
      field: fieldName,
      cellRenderer: 'customStyle',
      style: () => {
        return style ?? {};
      },
      sortable,
    };
    return colDef;
  },
};

export { AbsGrid };
