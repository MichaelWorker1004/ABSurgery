using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Billing
{
	public interface IGenerateTransactionTokenCommand : IYtgCommandBase<int>
	{
		public decimal Amount { get; }
		public string InvoiceNumber { get; }
		public int Quantity { get; }
		public string Description { get; }
		public decimal CostPerUnit { get; }
		public string FirstName { get; }
		public string LastName { get; }
		public string Email { get; }
		public string PhoneNumber { get; }
		public string AddressLine1 { get; }
		public string AddressLine2 { get; }
		public string City { get; }
		public string State { get; }
		public string ZipCode { get; }
		public string TransactionToken { get; }
	}
}
