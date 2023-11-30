using System;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Billing
{
	public interface ISubmitTransactionStatusCommandDal
	{
		Task SubmitTransactionTokenAsync(
			string transactionId, 
			string invoiceNumber, 
			string lastName, 
			string firstName, 
			decimal amount, 
			DateTime transactionTime, 
			string allParameters);
	}
}
