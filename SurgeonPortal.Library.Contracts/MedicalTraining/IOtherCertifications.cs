using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.MedicalTraining
{
    public interface IOtherCertifications : IYtgBusinessBase
    {
        int Id { get; set; }
        int UserId { get; set; }
        int CertificateTypeId { get; set; }
        string CertificateTypeName { get; set; }
        DateTime IssueDate { get; set; }
        string CertificateNumber { get; set; }
    }
}
