using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Billing
{
    public interface IApplicationFeeReadOnly : IYtgReadOnlyBase<int>
    {
        decimal? SubTotal { get; }
        string InvoiceNumber { get; }
        decimal? PaidTotal { get; }
        decimal? BalanceDue { get; }
        string TrackCode { get; }
        DateTime? PaymentDate { get; }
    }
}
