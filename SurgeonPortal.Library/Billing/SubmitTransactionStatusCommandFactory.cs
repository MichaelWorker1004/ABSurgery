using Csla;
using SurgeonPortal.Library.Contracts.Billing;
using System;
using System.Threading.Tasks;

namespace SurgeonPortal.Library.Billing
{
	public class SubmitTransactionStatusCommandFactory : ISubmitTransactionStatusCommandFactory
	{
		public async Task<ISubmitTransactionStatusCommand> SubmitTransactionStatusAsync(
			string method, 
			string param, 
			string resultMessage, 
			string userId, 
			string merchantId, 
			string lastName, 
			string firstName, 
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
			string cvv2Response)
		{
			if(!SubmitTransactionStatusCommand.CanExecuteCommand())
			{
				throw new System.Security.SecurityException("Not authorized to execute command");
			}

			var cmd = ApplicationContext.DataPortalActivator.CreateInstance(typeof(ISubmitTransactionStatusCommand))
				as SubmitTransactionStatusCommand;

			cmd.Method = method;
			cmd.Param = param;
			cmd.ResultMessage = resultMessage;
			cmd.UserId = userId;
			cmd.MerchantId = merchantId;
			cmd.LastName = lastName;
			cmd.FirstName = firstName;
			cmd.InvoiceNumber = invoiceNumber;
			cmd.Email = email;
			cmd.PhoneNumber = phoneNumber;
			cmd.Amount = amount;
			cmd.MerchantInitiatedUnscheduled = merchantInitiatedUnscheduled;
			cmd.PartnerAppId = partnerAppId;
			cmd.OarData = oarData;
			cmd.Result = result;
			cmd.TransactionId = transactionId;
			cmd.AvsResponse = avsResponse;
			cmd.ApprovalCode = approvalCode;
			cmd.TransactionTime = transactionTime;
			cmd.Tid = tid;
			cmd.ExpDate = expDate;
			cmd.GetToken = getToken;
			cmd.CardType = cardType;
			cmd.AssociationTokenData = associationTokenData;
			cmd.CardholderIp = cardholderIp;
			cmd.TransactionType = transactionType;
			cmd.BinNo = binNo;
			cmd.AccountBalance = accountBalance;
			cmd.Ps2000Data = ps2000Data;
			cmd.State = state;
			cmd.City = city;
			cmd.Cvv2Response = cvv2Response;

			return await DataPortal.ExecuteAsync(cmd);
		}
	}
}
