import { AbsGrid } from '../shared/components/grid/abs-grid';

export const PAYMENT_HISTORY_COLS = [
  AbsGrid.setCellCustomStyle('Invoice #', 'invoiceNumber', {
    color: '#1C827D',
    fontWeight: 'bold',
  }),
  AbsGrid.setCurrencyDisplayCol('Total Billed', 'totalBilled'),
  AbsGrid.setCurrencyDisplayCol('Paid', 'paid'),
  AbsGrid.setCurrencyDisplayCol('Balance', 'balanceDue'),
  AbsGrid.setCustomButtonCol('Pay Balance', 'balanceDue'),
];
