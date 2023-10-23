using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.ProfessionalStanding
{
    public interface IMedicalLicenseReadOnly : IYtgReadOnlyBase<int>
    {
        decimal LicenseId { get; }
        int? UserId { get; }
        string IssuingStateId { get; }
        string IssuingState { get; }
        string LicenseNumber { get; }
        int? LicenseTypeId { get; }
        string LicenseType { get; }
        DateTime? IssueDate { get; }
        DateTime? ExpireDate { get; }
        string ReportingOrganization { get; }
    }
}
