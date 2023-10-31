using Csla;
using SurgeonPortal.DataAccess.Contracts.Billing;
using SurgeonPortal.Library.Contracts.Billing;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;

namespace SurgeonPortal.Library.Billing
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
    [Serializable]
	[DataContract]
    public class ExamFeeReadOnly : YtgReadOnlyBase<ExamFeeReadOnly, int>, IExamFeeReadOnly
    {


        public ExamFeeReadOnly(IIdentityProvider identityProvider)
            : base(identityProvider)
        {
        }
        
        [DataMember]
		[DisplayName(nameof(SubTotal))]
        public decimal? SubTotal => ReadProperty(SubTotalProperty);
		public static readonly PropertyInfo<decimal?> SubTotalProperty = RegisterProperty<decimal?>(c => c.SubTotal);

        [DataMember]
		[DisplayName(nameof(InvoiceNumber))]
        public string InvoiceNumber => ReadProperty(InvoiceNumberProperty);
		public static readonly PropertyInfo<string> InvoiceNumberProperty = RegisterProperty<string>(c => c.InvoiceNumber);

        [DataMember]
		[DisplayName(nameof(PaidTotal))]
        public decimal? PaidTotal => ReadProperty(PaidTotalProperty);
		public static readonly PropertyInfo<decimal?> PaidTotalProperty = RegisterProperty<decimal?>(c => c.PaidTotal);

        [DataMember]
		[DisplayName(nameof(BalanceDue))]
        public decimal? BalanceDue => ReadProperty(BalanceDueProperty);
		public static readonly PropertyInfo<decimal?> BalanceDueProperty = RegisterProperty<decimal?>(c => c.BalanceDue);

        [DataMember]
		[DisplayName(nameof(ExamCode))]
        public string ExamCode => ReadProperty(ExamCodeProperty);
		public static readonly PropertyInfo<string> ExamCodeProperty = RegisterProperty<string>(c => c.ExamCode);

        [DataMember]
		[DisplayName(nameof(PaymentDate))]
        public DateTime? PaymentDate => ReadProperty(PaymentDateProperty);
		public static readonly PropertyInfo<DateTime?> PaymentDateProperty = RegisterProperty<DateTime?>(c => c.PaymentDate);


        [FetchChild]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private void Child_Fetch(ExamFeeReadOnlyDto dto)
        {
            FetchData(dto);
        }

        
		private void FetchData(ExamFeeReadOnlyDto dto)
		{
            LoadProperty(SubTotalProperty, dto.SubTotal);
            LoadProperty(InvoiceNumberProperty, dto.InvoiceNumber);
            LoadProperty(PaidTotalProperty, dto.PaidTotal);
            LoadProperty(BalanceDueProperty, dto.BalanceDue);
            LoadProperty(ExamCodeProperty, dto.ExamCode);
            LoadProperty(PaymentDateProperty, dto.PaymentDate);
		} 
        
    }
}
