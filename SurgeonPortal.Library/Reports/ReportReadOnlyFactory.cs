﻿using Csla;
using SurgeonPortal.Library.Contracts.Reports;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Reports
{
	public class ReportReadOnlyFactory : IReportReadOnlyFactory
	{
		public async Task<IReportReadOnly> GetByInvoiceNumber(string invoiceNumber)
		{
			if(string.IsNullOrWhiteSpace(invoiceNumber))
			{
				throw new FactoryInvalidCriteriaException("invoiceNumber is a required field");
			}

			return await DataPortal.FetchAsync<ReportReadOnly>(
				new GetByInvoiceNumberCriteria(invoiceNumber));
		}

		public async Task<IReportReadOnly> GetAdmissionCardByExamId(string examCode, string type)
		{
			if(string.IsNullOrWhiteSpace(examCode))
			{
				throw new FactoryInvalidCriteriaException("examId is a required field");
			}

			if(string.IsNullOrWhiteSpace(type))
			{
				throw new FactoryInvalidCriteriaException("type is a required field");
			}

			return await DataPortal.FetchAsync<ReportReadOnly>(
				new GetAdmissionCardByExamIdCriteria(examCode, type));
		}
	}

	[Serializable]
	internal class GetByInvoiceNumberCriteria : ReportCriteriaBase
	{
		public string InvoiceNumber { get; set; }

		public GetByInvoiceNumberCriteria(string invoiceNumber)
		{
			Type = "invoice";
			InvoiceNumber = invoiceNumber;
		}
	}

	[Serializable]
	internal class GetAdmissionCardByExamIdCriteria : ReportCriteriaBase
	{
		public string ExamCode { get; set; }

		public GetAdmissionCardByExamIdCriteria(string examCode, string type)
		{
			Type = type;
			ExamCode = examCode;
		}
	}

	internal class ReportCriteriaBase
	{
		public string Type { get; set; }
	}
}
