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
    sortable?: boolean,
    width?: number
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
      field: fieldName,
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
    sortable?: boolean
  ) {
    const colDef = {
      headerName: headerName,
      field: fieldName,
      cellRenderer: 'expandToggle',
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
