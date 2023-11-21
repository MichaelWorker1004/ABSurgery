using Csla;
using SurgeonPortal.Library.Contracts.Billing;
using System.Threading.Tasks;

namespace SurgeonPortal.Library.Billing
{
	public class GenerateTransactionTokenCommandFactory : IGenerateTransactionTokenCommandFactory
	{
		public async Task<IGenerateTransactionTokenCommand> GenerateTransactionTokenAsync(
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
			string zip)
		{
			if(!GenerateTransactionTokenCommand.CanExecuteCommand())
			{
				throw new System.Security.SecurityException("Not authorized to execute command");
			}

			var cmd = ApplicationContext.DataPortalActivator.CreateInstance(typeof(IGenerateTransactionTokenCommand))
				as GenerateTransactionTokenCommand;

			cmd.Amount = amount;
			cmd.InvoiceNumber = invoiceNumber;
			cmd.Quantity = quantity;
			cmd.Description = description;
			cmd.CostPerUnit = costPerUnit;
			cmd.FirstName = firstName;
			cmd.LastName = lastName;
			cmd.Email = email;
			cmd.PhoneNumber = phoneNumber;
			cmd.AddressLine1 = addressLine1;
			cmd.AddressLine2 = addressLine2;
			cmd.City = city;
			cmd.State = state;
			cmd.ZipCode = zip;

			return await DataPortal.ExecuteAsync(cmd);
		}
	}
}
