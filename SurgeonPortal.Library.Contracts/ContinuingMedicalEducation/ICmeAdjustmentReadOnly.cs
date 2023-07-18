using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.ContinuingMedicalEducation
{
    public interface ICmeAdjustmentReadOnly : IYtgReadOnlyBase
    {
        decimal CmeId { get; }
        int? UserId { get; }
        string Date { get; }
        string Description { get; }
        decimal CreditsTotal { get; }
        decimal? CreditsSA { get; }
        string IssuedBy { get; }
        DateTime CreditExpDate { get; }
    }
}
