using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Billing
{
    public interface IExamFeeReadOnly : IYtgReadOnlyBase
    {
        decimal? SubTotal { get; }
        string InvoiceNumber { get; }
        decimal? PaidTotal { get; }
        decimal? BalanceDue { get; }
        string ExamCode { get; }
        DateTime? PaymentDate { get; }
    }
}
