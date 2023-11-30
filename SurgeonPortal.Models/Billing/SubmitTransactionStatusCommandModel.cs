using Microsoft.AspNetCore.Mvc;
using System;

namespace SurgeonPortal.Models.Billing
{
	public class SubmitTransactionStatusCommandModel
	{
		[FromQuery]
		public string Method { get; set; }

		[FromQuery]
		public string Param { get; set; }

		[FromForm(Name = "ssl_result_message")]
		public string ResultMessage { get; set; }
		
		[FromForm(Name = "ssl_user_id")]
		public string UserId { get; set; }
		
		[FromForm(Name = "ssl_merchant_id")]
		public string MerchantId { get; set; }
		
		[FromForm(Name = "ssl_last_name")]
		public string LastName { get; set; }
		
		[FromForm(Name = "ssl_first_name")]
		public string FirstName { get; set; }
		
		[FromForm(Name = "ssl_invoice_number")]
		public string InvoiceNumber { get; set; }
		
		[FromForm(Name = "ssl_email")]
		public string Email { get; set; }
		
		[FromForm(Name = "ssl_phone")]
		public string PhoneNumber { get; set; }
		
		[FromForm(Name = "ssl_amount")]
		public decimal Amount { get; set; }
		
		[FromForm(Name = "ssl_merchant_initiated_unscheduled")]
		public string MerchantInitiatedUnscheduled { get; set; }
		
		[FromForm(Name = "ssl_partner_app_id")]
		public string PartnerAppId { get; set; }
		
		[FromForm(Name = "ssl_oar_data")]
		public string OarData { get; set; }
		
		[FromForm(Name = "ssl_result")]
		public string Result { get; set; }
		
		[FromForm(Name = "ssl_txn_id")]
		public string TransactionId { get; set; }
		
		[FromForm(Name = "ssl_avs_response")]
		public string AvsResponse { get; set; }
		
		[FromForm(Name = "ssl_approval_code")]
		public string ApprovalCode { get; set; }
		
		[FromForm(Name = "ssl_txn_time")]
		public DateTime TransactionTime { get; set; }
		
		[FromForm(Name = "ssl_tid")]
		public string Tid { get; set; }
		
		[FromForm(Name = "ssl_exp_date")]
		public string ExpDate { get; set; }
		
		[FromForm(Name = "ssl_get_token")]
		public string GetToken { get; set; }
		
		[FromForm(Name = "ssl_card_type")]
		public string CardType { get; set; }
		
		[FromForm(Name = "ssl_association_token_data")]
		public string AssociationTokenData { get; set; }
		
		[FromForm(Name = "ssl_cardholder_ip")]
		public string CardholderIp { get; set; }
		
		[FromForm(Name = "ssl_transaction_type")]
		public string TransactionType { get; set; }
		
		[FromForm(Name = "ssl_bin_no")]
		public string BinNo { get; set; }
		
		[FromForm(Name = "ssl_account_balance")]
		public string AccountBalance { get; set; }
		
		[FromForm(Name = "ssl_ps2000_data")]
		public string Ps2000Data { get; set; }
		
		[FromForm(Name = "ssl_state")]
		public string State { get; set; }
		
		[FromForm(Name = "ssl_city")]
		public string City { get; set; }
		
		[FromForm(Name = "ssl_cvv2_response")]
		public string Cvv2Response { get; set; }
	}
}
