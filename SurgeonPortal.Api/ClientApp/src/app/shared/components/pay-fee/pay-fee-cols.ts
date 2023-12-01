import { AbsGrid } from '../grid/abs-grid';

export const PAY_FEE_COLS = [
  AbsGrid.setTextDisplayCol('Invoice Number', 'invoiceNumber'),
  AbsGrid.setFormattedDateCol('Payment Date', 'paymentDate'),
  AbsGrid.setCurrencyDisplayCol('Payment Amount', 'paidTotal'),
  AbsGrid.setCurrencyDisplayCol('Remaining Balance', 'balanceDue'),
  AbsGrid.setCustomButtonCol('Pay Balance', 'balanceDue'),
  AbsGrid.setCustomButtonCol('Receipt', 'receipt', 'fa-solid fa-download'),
];
