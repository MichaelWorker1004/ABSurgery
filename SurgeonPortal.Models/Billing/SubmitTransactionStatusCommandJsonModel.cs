using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json.Serialization;

namespace SurgeonPortal.Models.Billing
{
	public class SubmitTransactionStatusCommandJsonModel
	{
		[FromQuery]
		public string Method { get; set; }

		[FromQuery]
		public string Param { get; set; }

		[JsonPropertyName("ssl_result_message")]
		public string ResultMessage { get; set; }
		
		[JsonPropertyName("ssl_user_id")]
		public string UserId { get; set; }
		
		[JsonPropertyName("ssl_merchant_id")]
		public string MerchantId { get; set; }
		
		[JsonPropertyName("ssl_last_name")]
		public string LastName { get; set; }
		
		[JsonPropertyName("ssl_first_name")]
		public string FirstName { get; set; }
		
		[JsonPropertyName("ssl_invoice_number")]
		public string InvoiceNumber { get; set; }
		
		[JsonPropertyName("ssl_email")]
		public string Email { get; set; }
		
		[JsonPropertyName("ssl_phone")]
		public string PhoneNumber { get; set; }
		
		[JsonPropertyName("ssl_amount")]
		public decimal Amount { get; set; }
		
		[JsonPropertyName("ssl_merchant_initiated_unscheduled")]
		public string MerchantInitiatedUnscheduled { get; set; }
		
		[JsonPropertyName("ssl_partner_app_id")]
		public string PartnerAppId { get; set; }
		
		[JsonPropertyName("ssl_oar_data")]
		public string OarData { get; set; }
		
		[JsonPropertyName("ssl_result")]
		public string Result { get; set; }
		
		[JsonPropertyName("ssl_txn_id")]
		public string TransactionId { get; set; }
		
		[JsonPropertyName("ssl_avs_response")]
		public string AvsResponse { get; set; }
		
		[JsonPropertyName("ssl_approval_code")]
		public string ApprovalCode { get; set; }
		
		[JsonPropertyName("ssl_txn_time")]
		public DateTime TransactionTime { get; set; }
		
		[JsonPropertyName("ssl_tid")]
		public string Tid { get; set; }
		
		[JsonPropertyName("ssl_exp_date")]
		public string ExpDate { get; set; }
		
		[JsonPropertyName("ssl_get_token")]
		public string GetToken { get; set; }
		
		[JsonPropertyName("ssl_card_type")]
		public string CardType { get; set; }
		
		[JsonPropertyName("ssl_association_token_data")]
		public string AssociationTokenData { get; set; }
		
		[JsonPropertyName("ssl_cardholder_ip")]
		public string CardholderIp { get; set; }
		
		[JsonPropertyName("ssl_transaction_type")]
		public string TransactionType { get; set; }
		
		[JsonPropertyName("ssl_bin_no")]
		public string BinNo { get; set; }
		
		[JsonPropertyName("ssl_account_balance")]
		public string AccountBalance { get; set; }
		
		[JsonPropertyName("ssl_ps2000_data")]
		public string Ps2000Data { get; set; }
		
		[JsonPropertyName("ssl_state")]
		public string State { get; set; }
		
		[JsonPropertyName("ssl_city")]
		public string City { get; set; }
		
		[JsonPropertyName("ssl_cvv2_response")]
		public string Cvv2Response { get; set; }
	}
}
