using System;

namespace SurgeonPortal.DataAccess.Contracts.MedicalTraining
{
    public class OtherCertificationsReadOnlyDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CertificateTypeId { get; set; }
        public string CertificateTypeName { get; set; }
        public DateTime IssueDate { get; set; }
        public string CertificateNumber { get; set; }
    }
}
