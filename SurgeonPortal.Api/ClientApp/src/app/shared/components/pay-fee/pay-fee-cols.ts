import { AbsGrid } from '../grid/abs-grid';

export const PAY_FEE_COLS = [
  AbsGrid.setFormattedDateCol('Payment Date', 'paymentDate'),
  AbsGrid.setTextDisplayCol('Payment Amount', 'paymentAmount'),
  AbsGrid.setTextDisplayCol('Remaining Balance', 'balanceRemaining'),
  AbsGrid.setCustomButtonCol('Receipt', 'receipt', 'fa-solid fa-download'),
];
