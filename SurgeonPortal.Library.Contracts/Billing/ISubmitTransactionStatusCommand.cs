using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Billing
{
	public interface ISubmitTransactionStatusCommand : IYtgCommandBase<int>
	{
		public string Method { get; }
		public string Param { get; }
		public string ResultMessage { get; }
		public string UserId { get; }
		public string MerchantId { get; }
		public string LastName { get; }
		public string FirstName { get; }
		public string InvoiceNumber { get; }
		public string Email { get; }
		public string PhoneNumber { get; }
		public decimal Amount { get; }
		public string MerchantInitiatedUnscheduled { get; }
		public string PartnerAppId { get; }
		public string OarData { get; }
		public string Result { get; }
		public string TransactionId { get; }
		public string AvsResponse { get; }
		public string ApprovalCode { get; }
		public DateTime TransactionTime { get; }
		public string Tid { get; }
		public string ExpDate { get; }
		public string GetToken { get; }
		public string CardType { get; }
		public string AssociationTokenData { get; }
		public string CardholderIp { get; }
		public string TransactionType { get; }
		public string BinNo { get; }
		public string AccountBalance { get; }
		public string Ps2000Data { get; }
		public string State { get; }
		public string City { get; }
		public string Cvv2Response { get; }
	}
}
