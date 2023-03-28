import { AbsGrid } from '../shared/components/grid/abs-grid';

export const PAY_FEE_COLS = [
  AbsGrid.setFormattedDateCol('Payment Date', 'paymentDate'),
  AbsGrid.setTextDisplayCol('Payment Amount', 'paymentAmount'),
  AbsGrid.setTextDisplayCol('Payment Method', 'balanceRemaining'),
  AbsGrid.setCustomButtonCol(
    'Download Receipt',
    'download',
    'fa-solid fa-download'
  ),
];
