export interface IExamFeeTransactionModel {
  amount: string;
  invoiceNumber: string;
  quantity: string;
  description: string;
  costPerUnit: string;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  addressLine1: string;
  addressLine2: string;
  city: string;
  state: string;
  zipCode: string;
  transactionToken: string;
  callbackUrl: string;
}