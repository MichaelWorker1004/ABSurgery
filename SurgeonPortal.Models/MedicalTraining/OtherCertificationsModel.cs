using System;
using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.MedicalTraining
{
    public class OtherCertificationsModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CertificateTypeId { get; set; }
        public string CertificateTypeName { get; set; }
        public DateTime IssueDate { get; set; }
        public string CertificateNumber { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime LastUpdatedAtUtc { get; set; }
        public int LastUpdatedByUserId { get; set; }
    }
}
