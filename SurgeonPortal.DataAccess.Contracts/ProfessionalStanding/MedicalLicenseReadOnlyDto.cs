using System;

namespace SurgeonPortal.DataAccess.Contracts.ProfessionalStanding
{
    public class MedicalLicenseReadOnlyDto
    {
        public decimal LicenseId { get; set; }
        public int? UserId { get; set; }
        public string IssuingStateId { get; set; }
        public string IssuingState { get; set; }
        public string LicenseNumber { get; set; }
        public int? LicenseTypeId { get; set; }
        public string LicenseType { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string ReportingOrganization { get; set; }
    }
}
