using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.MedicalTraining
{
    public class UserCertificateModel
    {
        public int CertificateId { get; set; }
        public int UserId { get; set; }
        public int DocumentId { get; set; }
        public int CertificateTypeId { get; set; }
        public string CertificateType { get; set; }
        public DateTime IssueDate { get; set; }
        public string CertificateNumber { get; set; }
        public IFormFile File { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime LastUpdatedAtUtc { get; set; }
        public int LastUpdatedByUserId { get; set; }
    }
}
