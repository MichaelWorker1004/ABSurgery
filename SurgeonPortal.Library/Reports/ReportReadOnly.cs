using Csla;
using SurgeonPortal.DataAccess.Contracts.Reports;
using SurgeonPortal.Library.Contracts.Reports;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;

namespace SurgeonPortal.Library.Reports
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	[DataContract]
	public class ReportReadOnly : YtgBusinessBase<ReportReadOnly>, IReportReadOnly
	{
		private readonly IReportDal _reportDal;

		public ReportReadOnly(IIdentityProvider identity,
			IReportDal reportDal) : base(identity)
		{
			_reportDal = reportDal;
		}

		public byte[] Data
		{
			get { return GetProperty(DataProperty); }
			set { SetProperty(DataProperty, value); }
		}
		public static readonly PropertyInfo<byte[]> DataProperty = RegisterProperty<byte[]>(c => c.Data);

		public string ContentType
		{
			get { return GetProperty(ContentTypeProperty); }
			set { SetProperty(ContentTypeProperty, value); }
		}
		public static readonly PropertyInfo<string> ContentTypeProperty = RegisterProperty<string>(c => c.ContentType);

		[Fetch]
		[RunLocal]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
		   Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
		private async Task GetByInvoiceNumber(GetByInvoiceNumberCriteria criteria)
		{
			var query = $"{GetBaseQuery(criteria)}&inv_num={criteria.InvoiceNumber}";
			await GetFromQuery(query);
		}

		[Fetch]
		[RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
		private async Task GetAdmissionCardByExamId(GetAdmissionCardByExamIdCriteria criteria)
		{
			var userId = _identity.GetUserId<int>();
			var query = $"{GetBaseQuery(criteria)}&examcode={criteria.ExamId}&candidate={userId}";
			await GetFromQuery(query);
		}

        private static string GetBaseQuery(ReportCriteriaBase criteria)
		{
			return $"?jr={criteria.Type}";
		}

		private async Task GetFromQuery(string query)
		{
			using(BypassPropertyChecks)
			{
				var dto = await _reportDal.GetReportAsync(query);

				if(dto == null)
				{
					throw new Ytg.Framework.Exceptions.DataNotFoundException("Report not found based on criteria");
				}

				FetchData(dto);
			}
		}

		private void FetchData(ReportDto dto)
		{
			base.FetchData(dto);

			this.Data = dto.Data;
			this.ContentType = dto.ContentType;
		}
	}
}
