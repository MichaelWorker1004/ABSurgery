using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using SurgeonPortal.Library.Contracts.Billing;
using SurgeonPortal.Models.Billing;
using System.Text.Json;
using System.Text.Json.Serialization;
using Ytg.AspNetCore.Controllers;

namespace SurgeonPortal.Api.Controllers.Billing
{
	[ApiVersion("1")]
	[ApiController]
	[Produces("application/json")]
	[Route("api/billing/transaction")]
	public class TransactionController : YtgControllerBase
	{
		private readonly ISendGridClient _client;
		public TransactionController(IWebHostEnvironment webHostEnvironment, ISendGridClient client) 
			: base(webHostEnvironment)
		{
			_client = client;
		}

		[MapToApiVersion("1")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenerateTransactionTokenCommandModel))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[HttpPost("token")]
		public async Task<ActionResult<GenerateTransactionTokenCommandModel>> GetTransactionTokenCommand(
			[FromServices] IGenerateTransactionTokenCommandFactory generateTransactionTokenCommandFactory,
			[FromBody] GenerateTransactionTokenCommandModel model)
		{
			if(model == null)
			{
				return BadRequest("Request payload could not be bound to model. Are you missing fields? Are you passing the correct datatypes?");
			}

			var command = await generateTransactionTokenCommandFactory.GenerateTransactionTokenAsync(model.Amount, model.InvoiceNumber, model.Quantity, model.Description, model.CostPerUnit, model.FirstName, model.LastName, model.Email, model.PhoneNumber, model.AddressLine1, model.AddressLine2, model.City, model.State, model.ZipCode, model.CallbackUrl);

			model.TransactionToken = command.TransactionToken;

			return Ok(model);
		}

		[AllowAnonymous]
		[MapToApiVersion("1")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[Consumes("application/json")]
		[HttpPost("callback")]
		public async Task<ActionResult> SubmitTransactionStatusJsonCommand(
			[FromServices] ISubmitTransactionStatusCommandFactory submitTransactionStatusCommandFactory,
			[FromBody] SubmitTransactionStatusCommandJsonModel model)
		{
			await SendEmail("json", JsonSerializer.Serialize(model));

			if (model == null)
			{
				return BadRequest("Request payload could not be bound to model. Are you missing fields? Are you passing the correct datatypes?");
			}

			var command = await submitTransactionStatusCommandFactory.SubmitTransactionStatusAsync(model.Method, model.Param, model.ResultMessage, model.UserId, model.MerchantId, model.LastName, model.FirstName, model.InvoiceNumber, model.Email, model.PhoneNumber, model.Amount, model.MerchantInitiatedUnscheduled, model.PartnerAppId, model.OarData, model.Result, model.TransactionId, model.AvsResponse, model.ApprovalCode, model.TransactionTime, model.Tid, model.ExpDate, model.GetToken, model.CardType, model.AssociationTokenData, model.CardholderIp, model.TransactionType, model.BinNo, model.AccountBalance, model.Ps2000Data, model.State, model.City, model.Cvv2Response);

			return Ok();
		}

		[AllowAnonymous]
		[MapToApiVersion("1")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[Consumes("multipart/form-data", "application/x-www-form-urlencoded")]
		[HttpPost("callback")]
		public async Task<ActionResult> SubmitTransactionStatusCommand(
			[FromServices] ISubmitTransactionStatusCommandFactory submitTransactionStatusCommandFactory,
			[FromForm] SubmitTransactionStatusCommandModel model)
		{
			await SendEmail("form data", JsonSerializer.Serialize(model));

			if(model == null)
			{
				return BadRequest("Request payload could not be bound to model. Are you missing fields? Are you passing the correct datatypes?");
			}

			var command = await submitTransactionStatusCommandFactory.SubmitTransactionStatusAsync(model.Method, model.Param, model.ResultMessage, model.UserId, model.MerchantId, model.LastName, model.FirstName, model.InvoiceNumber, model.Email, model.PhoneNumber, model.Amount, model.MerchantInitiatedUnscheduled, model.PartnerAppId, model.OarData, model.Result, model.TransactionId, model.AvsResponse, model.ApprovalCode, model.TransactionTime, model.Tid, model.ExpDate, model.GetToken, model.CardType, model.AssociationTokenData, model.CardholderIp, model.TransactionType, model.BinNo, model.AccountBalance, model.Ps2000Data, model.State, model.City, model.Cvv2Response);

			return Ok();
		}

		[MapToApiVersion("1")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[HttpGet("getip")]
		public async Task<ActionResult> GetIp()
		{
			var client = new HttpClient()
			{
				BaseAddress = new Uri("https://www.convergepay.com/hosted-payments/myip")
			};

			var response = await client.GetAsync("");

			var content = await response.Content.ReadAsStringAsync();

			return Ok(content);
		}

		private async Task SendEmail(string location, string body)
		{
			var toEmails = new List<EmailAddress>
			{
				new EmailAddress("evan.frisch@ytg.io"),
				new EmailAddress("joseph.mayberry@ytg.io"),
				new EmailAddress("daniel.sarria@ytg.io")
			};

			var msg = MailHelper.CreateSingleEmailToMultipleRecipients(new EmailAddress("notifications@absurgery.org"), toEmails, $"Callback received - {location}", body, null);
			await _client.SendEmailAsync(msg);
		}
	}
}
