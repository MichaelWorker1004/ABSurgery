namespace SurgeonPortal.Models.Billing
{
	public class GenerateTransactionTokenCommandModel
	{
		public decimal Amount { get; set; }
		public string InvoiceNumber { get; set; }
		public int Quantity { get; set; }
		public string Description { get; set; }
		public decimal CostPerUnit { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string AddressLine1 { get; set; }
		public string AddressLine2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string ZipCode { get; set; }
		public string TransactionToken { get; set; }
	}
}
