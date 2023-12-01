using SurgeonPortal.DataAccess.Contracts.Billing;
using System;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Billing
{
	public class SubmitTransactionStatusCommandDal : SqlServerDalBase, ISubmitTransactionStatusCommandDal
	{
		public SubmitTransactionStatusCommandDal(ISqlConnectionManager sqlConnectionManager) 
			: base(sqlConnectionManager)
		{
		}

		public async Task SubmitTransactionTokenAsync(string transactionId, string invoiceNumber, string lastName, string firstName, decimal amount, DateTime transactionTime, string allParameters)
		{
			using (var connection = CreateConnection())
			{
				await connection.ExecAsync(
					"[dbo].[ins_payment]",
					new
					{
						sslTransactionId = transactionId,
						sslInvoiceNumber = invoiceNumber,
						sslLastName = lastName,
						sslFirstName = firstName,
						sslAmount = amount,
						sslTxnTime = transactionTime,
						sslAllParameters = allParameters
					});
			}
		}
	}
}
