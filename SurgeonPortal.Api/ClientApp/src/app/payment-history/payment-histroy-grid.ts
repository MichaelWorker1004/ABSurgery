import { AbsGrid } from '../shared/components/grid/abs-grid';

export const PAYMENT_HISTORY_COLS = [
  AbsGrid.setCellCustomStyle('Invoice #', 'invoiceNumber', {
    color: '#1C827D',
    fontWeight: 'bold',
  }),
  AbsGrid.setTextDisplayCol('Fee Code', 'feeCode'),
  AbsGrid.setTextDisplayCol('Description', 'description'),
  AbsGrid.setTextDisplayCol('Total Billed', 'totalBilled'),
  AbsGrid.setTextDisplayCol('Paid', 'paid'),
  AbsGrid.setCellCustomStyle('Balance', 'balance', {
    color: '#1F3758',
    fontWeight: 'bold',
  }),
];
