using Microsoft.Extensions.Options;
using SurgeonPortal.DataAccess.Contracts.Billing;
using SurgeonPortal.Shared.PaymentProvider;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Billing
{
	public class GenerateTransactionTokenCommandDal : IGenerateTransactionTokenCommandDal
	{
		private readonly PaymentProviderConfiguration _paymentProviderConfiguration;

		public GenerateTransactionTokenCommandDal(IOptions<PaymentProviderConfiguration> options)
		{
			_paymentProviderConfiguration = options.Value;
		}

		public async Task<GenerateTransactionTokenCommandDto> GenerateTransactionTokenAsync(decimal amount, string invoiceNumber, int quantity, string description, decimal costPerUnit, string firstName, string lastName, string email, string phoneNumber, string addressLine1, string addressLine2, string city, string state, string zip, string callbackUrl)
		{
			var dto = new GenerateTransactionTokenCommandDto();

			var httpClient = new HttpClient
			{
				BaseAddress = new Uri(_paymentProviderConfiguration.Url)
			};

			var requestContent = new Dictionary<string, string>
			{
				{ "ssl_merchant_id", _paymentProviderConfiguration.MerchantId },
				{ "ssl_user_id", _paymentProviderConfiguration.UserId },
				{ "ssl_pin", _paymentProviderConfiguration.Pin },
				{ "ssl_transaction_type", "CCSALE" },
				{ "ssl_amount", amount.ToString() },
				{ "ssl_invoice_number", invoiceNumber },
				{ "ssl_line_Item_quantity", quantity.ToString() },
				{ "ssl_line_Item_description", description },
				{ "ssl_line_Item_unit_cost", costPerUnit.ToString() },
				{ "ssl_first_name", firstName },
				{ "ssl_last_name", lastName },
				{ "ssl_email", email },
				{ "ssl_phone", phoneNumber },
				{ "ssl_avs_address", addressLine1 },
				{ "ssl_address2", addressLine2 },
				{ "ssl_city", city },
				{ "ssl_state", state },
				{ "ssl_avs_zip", zip },
				{ "ssl_callback_url", callbackUrl }
			};

			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Post,
				Content = new FormUrlEncodedContent(requestContent)
			};

			var response = await httpClient.SendAsync(request);

			if (!response.IsSuccessStatusCode)
			{
				throw new HttpRequestException("Error generating transaction token.");
			}

			dto.TransactionToken = await response.Content.ReadAsStringAsync();

			return dto;
		}
	}
}
