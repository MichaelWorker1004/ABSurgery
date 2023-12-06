using Csla;
using Microsoft.Extensions.Options;
using SurgeonPortal.DataAccess.Contracts.Billing;
using SurgeonPortal.Library.Contracts.Billing;
using SurgeonPortal.Library.Contracts.Email;
using SurgeonPortal.Shared.PaymentProvider;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;

namespace SurgeonPortal.Library.Billing
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	public class SubmitTransactionStatusCommand : YtgCommandBase<SubmitTransactionStatusCommand, int>, ISubmitTransactionStatusCommand
	{
		private readonly PaymentProviderConfiguration _paymentProviderConfiguration;
		private readonly ISubmitTransactionStatusCommandDal _submitTransactionStatusCommandDal;
		private readonly IEmailFactory _emailFactory;

		public SubmitTransactionStatusCommand(
			IIdentityProvider identity,
			ISubmitTransactionStatusCommandDal submitTransactionStatusCommandDal,
			IEmailFactory emailFactory,
			IOptions<PaymentProviderConfiguration> paymentProviderConfiguration) : base(identity)
		{
			_submitTransactionStatusCommandDal = submitTransactionStatusCommandDal;
			_emailFactory = emailFactory;
			_paymentProviderConfiguration = paymentProviderConfiguration.Value;
		}

		public static bool CanExecuteCommand()
		{
			return true;
		}

		public string Method
		{
			get { return ReadProperty(MethodProperty); }
			internal set { LoadProperty(MethodProperty, value); }
		}
		public static readonly PropertyInfo<string> MethodProperty = RegisterProperty<string>(c => c.Method);

		public string Param
		{
			get { return ReadProperty(ParamProperty); }
			internal set { LoadProperty(ParamProperty, value); }
		}
		public static readonly PropertyInfo<string> ParamProperty = RegisterProperty<string>(c => c.Param);

		public string ResultMessage
		{
			get { return ReadProperty(ResultMessageProperty); }
			internal set { LoadProperty(ResultMessageProperty, value); }
		}
		public static readonly PropertyInfo<string> ResultMessageProperty = RegisterProperty<string>(c => c.ResultMessage);

		public string UserId
		{
			get { return ReadProperty(UserIdProperty); }
			internal set { LoadProperty(UserIdProperty, value); }
		}
		public static readonly PropertyInfo<string> UserIdProperty = RegisterProperty<string>(c => c.UserId);

		public string MerchantId
		{
			get { return ReadProperty(MerchantIdProperty); }
			internal set { LoadProperty(MerchantIdProperty, value); }
		}
		public static readonly PropertyInfo<string> MerchantIdProperty = RegisterProperty<string>(c => c.MerchantId);

		public string LastName
		{
			get { return ReadProperty(LastNameProperty); }
			internal set { LoadProperty(LastNameProperty, value); }
		}
		public static readonly PropertyInfo<string> LastNameProperty = RegisterProperty<string>(c => c.LastName);

		public string FirstName
		{
			get { return ReadProperty(FirstNameProperty); }
			internal set { LoadProperty(FirstNameProperty, value); }
		}
		public static readonly PropertyInfo<string> FirstNameProperty = RegisterProperty<string>(c => c.FirstName);

		public string InvoiceNumber
		{
			get { return ReadProperty(InvoiceNumberProperty); }
			internal set { LoadProperty(InvoiceNumberProperty, value); }
		}
		public static readonly PropertyInfo<string> InvoiceNumberProperty = RegisterProperty<string>(c => c.InvoiceNumber);

		public string Email
		{
			get { return ReadProperty(EmailProperty); }
			internal set { LoadProperty(EmailProperty, value); }
		}
		public static readonly PropertyInfo<string> EmailProperty = RegisterProperty<string>(c => c.Email);

		public string PhoneNumber
		{
			get { return ReadProperty(PhoneNumberProperty); }
			internal set { LoadProperty(PhoneNumberProperty, value); }
		}
		public static readonly PropertyInfo<string> PhoneNumberProperty = RegisterProperty<string>(c => c.PhoneNumber);

		public decimal Amount
		{
			get { return ReadProperty(AmountProperty); }
			internal set { LoadProperty(AmountProperty, value); }
		}
		public static readonly PropertyInfo<decimal> AmountProperty = RegisterProperty<decimal>(c => c.Amount);

		public string MerchantInitiatedUnscheduled
		{
			get { return ReadProperty(MerchantInitiatedUnscheduledProperty); }
			internal set { LoadProperty(MerchantInitiatedUnscheduledProperty, value); }
		}
		public static readonly PropertyInfo<string> MerchantInitiatedUnscheduledProperty = RegisterProperty<string>(c => c.MerchantInitiatedUnscheduled);

		public string PartnerAppId
		{
			get { return ReadProperty(PartnerAppIdProperty); }
			internal set { LoadProperty(PartnerAppIdProperty, value); }
		}
		public static readonly PropertyInfo<string> PartnerAppIdProperty = RegisterProperty<string>(c => c.PartnerAppId);

		public string OarData
		{
			get { return ReadProperty(OarDataProperty); }
			internal set { LoadProperty(OarDataProperty, value); }
		}
		public static readonly PropertyInfo<string> OarDataProperty = RegisterProperty<string>(c => c.OarData);

		public string Result
		{
			get { return ReadProperty(ResultProperty); }
			internal set { LoadProperty(ResultProperty, value); }
		}
		public static readonly PropertyInfo<string> ResultProperty = RegisterProperty<string>(c => c.Result);

		public string TransactionId
		{
			get { return ReadProperty(TransactionIdProperty); }
			internal set { LoadProperty(TransactionIdProperty, value); }
		}
		public static readonly PropertyInfo<string> TransactionIdProperty = RegisterProperty<string>(c => c.TransactionId);

		public string AvsResponse
		{
			get { return ReadProperty(AvsResponseProperty); }
			internal set { LoadProperty(AvsResponseProperty, value); }
		}
		public static readonly PropertyInfo<string> AvsResponseProperty = RegisterProperty<string>(c => c.AvsResponse);

		public string ApprovalCode
		{
			get { return ReadProperty(ApprovalCodeProperty); }
			internal set { LoadProperty(ApprovalCodeProperty, value); }
		}
		public static readonly PropertyInfo<string> ApprovalCodeProperty = RegisterProperty<string>(c => c.ApprovalCode);

		public DateTime TransactionTime
		{
			get { return ReadProperty(TransactionTimeProperty); }
			internal set { LoadProperty(TransactionTimeProperty, value); }
		}
		public static readonly PropertyInfo<DateTime> TransactionTimeProperty = RegisterProperty<DateTime>(c => c.TransactionTime);

		public string Tid
		{
			get { return ReadProperty(TidProperty); }
			internal set { LoadProperty(TidProperty, value); }
		}
		public static readonly PropertyInfo<string> TidProperty = RegisterProperty<string>(c => c.Tid);

		public string ExpDate
		{
			get { return ReadProperty(ExpDateProperty); }
			internal set { LoadProperty(ExpDateProperty, value); }
		}
		public static readonly PropertyInfo<string> ExpDateProperty = RegisterProperty<string>(c => c.ExpDate);

		public string GetToken
		{
			get { return ReadProperty(GetTokenProperty); }
			internal set { LoadProperty(GetTokenProperty, value); }
		}
		public static readonly PropertyInfo<string> GetTokenProperty = RegisterProperty<string>(c => c.GetToken);

		public string CardType
		{
			get { return ReadProperty(CardTypeProperty); }
			internal set { LoadProperty(CardTypeProperty, value); }
		}
		public static readonly PropertyInfo<string> CardTypeProperty = RegisterProperty<string>(c => c.CardType);

		public string AssociationTokenData
		{
			get { return ReadProperty(AssociationTokenDataProperty); }
			internal set { LoadProperty(AssociationTokenDataProperty, value); }
		}
		public static readonly PropertyInfo<string> AssociationTokenDataProperty = RegisterProperty<string>(c => c.AssociationTokenData);

		public string CardholderIp
		{
			get { return ReadProperty(CardholderIpProperty); }
			internal set { LoadProperty(CardholderIpProperty, value); }
		}
		public static readonly PropertyInfo<string> CardholderIpProperty = RegisterProperty<string>(c => c.CardholderIp);

		public string TransactionType
		{
			get { return ReadProperty(TransactionTypeProperty); }
			internal set { LoadProperty(TransactionTypeProperty, value); }
		}
		public static readonly PropertyInfo<string> TransactionTypeProperty = RegisterProperty<string>(c => c.TransactionType);

		public string BinNo
		{
			get { return ReadProperty(BinNoProperty); }
			internal set { LoadProperty(BinNoProperty, value); }
		}
		public static readonly PropertyInfo<string> BinNoProperty = RegisterProperty<string>(c => c.BinNo);

		public string AccountBalance
		{
			get { return ReadProperty(AccountBalanceProperty); }
			internal set { LoadProperty(AccountBalanceProperty, value); }
		}
		public static readonly PropertyInfo<string> AccountBalanceProperty = RegisterProperty<string>(c => c.AccountBalance);

		public string Ps2000Data
		{
			get { return ReadProperty(Ps2000DataProperty); }
			internal set { LoadProperty(Ps2000DataProperty, value); }
		}
		public static readonly PropertyInfo<string> Ps2000DataProperty = RegisterProperty<string>(c => c.Ps2000Data);

		public string State
		{
			get { return ReadProperty(StateProperty); }
			internal set { LoadProperty(StateProperty, value); }
		}
		public static readonly PropertyInfo<string> StateProperty = RegisterProperty<string>(c => c.State);

		public string City
		{
			get { return ReadProperty(CityProperty); }
			internal set { LoadProperty(CityProperty, value); }
		}
		public static readonly PropertyInfo<string> CityProperty = RegisterProperty<string>(c => c.City);

		public string Cvv2Response
		{
			get { return ReadProperty(Cvv2ResponseProperty); }
			internal set { LoadProperty(Cvv2ResponseProperty, value); }
		}
		public static readonly PropertyInfo<string> Cvv2ResponseProperty = RegisterProperty<string>(c => c.Cvv2Response);

		[Execute]
		protected async Task ExecuteCommand()
		{
			var email = _emailFactory.Create();

			if (ResultMessage.Equals("approval", StringComparison.InvariantCultureIgnoreCase))
			{
				var allFields = JsonSerializer.Serialize(this);
				await _submitTransactionStatusCommandDal.SubmitTransactionTokenAsync(TransactionId, InvoiceNumber, LastName, FirstName, Amount, TransactionTime, allFields);

				string subject;
				string text;

				if(TransactionType.Equals("return", StringComparison.InvariantCultureIgnoreCase))
				{
					subject = $"Receipt for Credit-Card Refund of ABS Invoice #{InvoiceNumber}";
					text = $"The American Board of Surgery has issued a refund of your payment toward Invoice #{InvoiceNumber}.\r\n\r\nRefunds take 7 to 10 business days to be credited to your credit card account. If you have a question or concern about your refund, please contact your credit card company directly.";
				}
				else
				{
					subject = $"Receipt for Credit-Card Payment of ABS Invoice #{InvoiceNumber}";
					text = $"Thank you for completing your payment to the American Board of Surgery. Attached, please find a receipt for your payment toward ABS Invoice #{InvoiceNumber}";
				}

				email.To = Email;
				email.Subject = subject;
				email.PlainTextContent = text;

				await email.SendAsync();
			}
			else
			{
				email.To = _paymentProviderConfiguration.ErrorEmailRecipient;
				email.Subject = "Transaction Failed";
				email.PlainTextContent = $"A transaction has failed.\r\n\r\n{JsonSerializer.Serialize(this)}";

				await email.SendAsync();
			}
		}
	}
}
