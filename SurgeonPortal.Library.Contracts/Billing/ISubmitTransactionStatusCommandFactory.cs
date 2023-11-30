using System;
using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Billing
{
	public interface ISubmitTransactionStatusCommandFactory
	{
		Task<ISubmitTransactionStatusCommand> SubmitTransactionStatusAsync(
			string method,
			string param,
			string resultMessage,
			string userId,
			string merchantId,
			string lastName,
			string firtName,
			string invoiceNumber,
			string email,
			string phoneNumber,
			decimal amount,
			string merchantInitiatedUnscheduled,
			string partnerAppId,
			string oarData,
			string result,
			string transactionId,
			string avsResponse,
			string approvalCode,
			DateTime transactionTime,
			string tid,
			string expDate,
			string getToken,
			string cardType,
			string associationTokenData,
			string cardholderIp,
			string transactionType,
			string binNo,
			string accountBalance,
			string ps2000Data,
			string state,
			string city,
			string cvv2Response);
	}
}
