using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.ProfessionalStanding
{
    public interface IMedicalLicense : IYtgBusinessBase
    {
        decimal LicenseId { get; set; }
        int? UserId { get; set; }
        string IssuingStateId { get; set; }
        string IssuingState { get; set; }
        string LicenseNumber { get; set; }
        int? LicenseTypeId { get; set; }
        string LicenseType { get; set; }
        DateTime? IssueDate { get; set; }
        DateTime? ExpireDate { get; set; }
        string ReportingOrganization { get; set; }
    }
}
