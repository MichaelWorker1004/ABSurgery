using System;

namespace SurgeonPortal.Models.MedicalTraining
{
    public class OtherCertificationsReadOnlyModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CertificateTypeId { get; set; }
        public string CertificateTypeName { get; set; }
        public DateTime IssueDate { get; set; }
        public string CertificateNumber { get; set; }
    }
}
