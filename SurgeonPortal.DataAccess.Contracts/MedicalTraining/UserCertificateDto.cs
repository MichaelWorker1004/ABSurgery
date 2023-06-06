using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.DataAccess.Contracts.MedicalTraining
{
    public class UserCertificateDto : YtgBusinessBaseDto
    {
        public int CertificateId { get; set; }
        public int UserId { get; set; }
        public int DocumentId { get; set; }
        public int CertificateTypeId { get; set; }
        public string CertificateType { get; set; }
        public DateTime IssueDate { get; set; }
        public string CertificateNumber { get; set; }
    }
}
