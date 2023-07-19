const AbsGrid = {
  setTextDisplayCol(
    headerName: string,
    fieldName: string,
    sortable?: boolean,
    width?: number
  ) {
    const colDef = {
      headerName: headerName,
      field: fieldName,
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
      field: fieldName,
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
      field: fieldName,
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
      field: fieldName,
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
    sortable?: boolean,
    width?: number
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
      field: fieldName,
      cellRenderer: 'dateFormatter',
      sortable,
      width,
    };
    return colDef;
  },

  setCellCustomClass(
    headerName: string,
    fieldName: string,
    sortable?: boolean,
    className?: string,
    width?: number
  ) {
    const colDef = {
      headerName,
      field: fieldName,
      cellRenderer: 'customClass',
      cellClass: (fieldName: string) => {
        return className ?? fieldName;
      },
      sortable,
      width,
    };
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
      field: fieldName,
      cellRenderer: 'customStyle',
      style: () => {
        return style ?? {};
      },
      sortable,
      width,
    };
    return colDef;
  },
};

export { AbsGrid };
