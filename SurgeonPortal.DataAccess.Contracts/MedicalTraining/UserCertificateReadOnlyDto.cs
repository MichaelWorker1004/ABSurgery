using System;

namespace SurgeonPortal.DataAccess.Contracts.MedicalTraining
{
    public class UserCertificateReadOnlyDto
    {
        public int CertificateId { get; set; }
        public int UserId { get; set; }
        public int DocumentId { get; set; }
        public int CertificateTypeId { get; set; }
        public string CertificateType { get; set; }
        public DateTime IssueDate { get; set; }
        public string CertificateNumber { get; set; }
        public string DocumentName { get; set; }
        public DateTime UploadDateUtc { get; set; }
    }
}
