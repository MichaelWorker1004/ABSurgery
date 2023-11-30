using Microsoft.AspNetCore.Mvc;
using SurgeonPortal.Library.Contracts.Billing;
using SurgeonPortal.Models.Billing;
using Ytg.AspNetCore.Controllers;

namespace SurgeonPortal.Api.Controllers.Billing
{
	[ApiVersion("1")]
	[ApiController]
	[Produces("application/json")]
	[Route("api/billing/transaction")]
	public class TransactionController : YtgControllerBase
	{
		public TransactionController(IWebHostEnvironment webHostEnvironment) 
			: base(webHostEnvironment)
		{
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

		[MapToApiVersion("1")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[HttpPost("callback")]
		public async Task<ActionResult> SubmitTransactionStatusCommand(
			[FromServices] ISubmitTransactionStatusCommandFactory submitTransactionStatusCommandFactory,
			[FromForm] SubmitTransactionStatusCommandModel model)
		{
			if(model == null)
			{
				return BadRequest("Request payload could not be bound to model. Are you missing fields? Are you passing the correct datatypes?");
			}

			var command = await submitTransactionStatusCommandFactory.SubmitTransactionStatusAsync(model.Method, model.Param, model.ResultMessage, model.UserId, model.MerchantId, model.LastName, model.FirstName, model.InvoiceNumber, model.Email, model.PhoneNumber, model.Amount, model.MerchantInitiatedUnscheduled, model.PartnerAppId, model.OarData, model.Result, model.TransactionId, model.AvsResponse, model.ApprovalCode, model.TransactionTime, model.Tid, model.ExpDate, model.GetToken, model.CardType, model.AssociationTokenData, model.CardholderIp, model.TransactionType, model.BinNo, model.AccountBalance, model.Ps2000Data, model.State, model.City, model.Cvv2Response);

			return Ok();
		}
	}
}
