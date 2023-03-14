import { Injectable } from '@angular/core';

@Injectable()
export class ExcelExporterService {
  public tableToXLS(data: any[], columns, fileName: string) {
    let table = document.createElement('table');

    let tableHead = table.createTHead();
    let tableHeadRow = tableHead.insertRow();

    let validColumns = {};
    for (let headColumn of columns) {
      let columnDisplayField = headColumn.displayField;
      let tableCell = tableHeadRow.insertCell();

      let tableCellTextSpan = document.createElement('span');

      tableCellTextSpan.appendChild(
        document.createTextNode(columnDisplayField)
      );
      tableCellTextSpan.style.color = 'black';
      tableCellTextSpan.style.fontWeight = 'bold';

      tableCell.appendChild(tableCellTextSpan);

      validColumns[headColumn.field] = true;
    }

    let tableBody = table.createTBody();
    for (let rowData of data) {
      let tableRow = tableBody.insertRow();

      for (let elementKey in rowData) {
        if (!validColumns[elementKey]) {
          continue;
        }
        let elementData = rowData[elementKey];
        if (elementData === null) {
          elementData = '';
        }

        let tableCell = tableRow.insertCell();
        tableCell.appendChild(document.createTextNode(elementData));
      }
    }
    this.downloadXLS(fileName, table);
  }

  private downloadXLS(fileName, tableContents) {
    let excelData = {
      worksheet: fileName,
      table: tableContents.innerHTML,
    };

    let template = `
            <html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40">
                <head>
                    <!--[if gte mso 9]>
                        <xml>
                            <x:ExcelWorkbook>
                                <x:ExcelWorksheets>
                                    <x:ExcelWorksheet>
                                        <x:Name>${excelData.worksheet}</x:Name>
                                        <x:WorksheetOptions>
                                            <x:DisplayGridlines/>
                                        </x:WorksheetOptions>
                                    </x:ExcelWorksheet>
                                </x:ExcelWorksheets>
                            </x:ExcelWorkbook>
                        </xml>
                    <![endif]-->
                </head>
                <body>
                    <table>
                        ${excelData.table}
                    </table>
                </body>
            </html>
        `;

    fileName = fileName + '.xls';
    let excelFile = document.createElement('a');
    excelFile.download = fileName;
    excelFile.href =
      'data:application/vnd.ms-excel;base64,' +
      btoa(unescape(encodeURIComponent(template)));
    excelFile.click();
  }

  public tableToCSV(data: any, columns, fileName: string) {
    fileName = fileName + '.csv';

    const replacer = (key, value) => (value === null ? '' : value);
    const header = columns.map((column) => column.field);
    const csv = data.map((row) =>
      header
        .map((fieldName) => JSON.stringify(row[fieldName], replacer))
        .join(',')
    );
    csv.unshift(columns.map((column) => column.displayField).join(','));
    const csvFile = csv.join('\r\n');

    // Download the file as csv
    const blob = new Blob(['\ufeff', csvFile], {
      type: 'text/csv',
    });
    const downloadLink = document.createElement('a');
    const url = URL.createObjectURL(blob);
    downloadLink.href = url;
    downloadLink.download = fileName;
    document.body.appendChild(downloadLink);
    downloadLink.click();
    document.body.removeChild(downloadLink);
  }
}
