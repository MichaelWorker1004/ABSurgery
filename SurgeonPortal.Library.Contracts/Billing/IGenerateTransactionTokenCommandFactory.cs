using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Billing
{
	public interface IGenerateTransactionTokenCommandFactory
	{
		Task<IGenerateTransactionTokenCommand> GenerateTransactionTokenAsync(
			decimal amount,
			string invoiceNumber,
			int quantity,
			string description,
			decimal costPerUnit,
			string firstName,
			string lastName,
			string email,
			string phoneNumber,
			string addressLine1,
			string addressLine2,
			string city,
			string state,
			string zip,
			string callbackUrl);
	}
}
