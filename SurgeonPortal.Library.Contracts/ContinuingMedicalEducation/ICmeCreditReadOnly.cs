using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.ContinuingMedicalEducation
{
    public interface ICmeCreditReadOnly : IYtgReadOnlyBase<int>
    {
        decimal CmeId { get; }
        int? UserId { get; }
        string Date { get; }
        string Description { get; }
        decimal CreditsTotal { get; }
        decimal? CreditsSA { get; }
        int CMEDirect { get; }
        DateTime CreditExpDate { get; }
    }
}
