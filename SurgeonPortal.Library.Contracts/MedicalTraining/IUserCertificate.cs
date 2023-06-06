using SurgeonPortal.Library.Contracts.Documents;
using System;
using System.IO;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.MedicalTraining
{
    public interface IUserCertificate : IYtgBusinessBase
    {
        int CertificateId { get; set; }
        int UserId { get; set; }
        int DocumentId { get; set; }
        int CertificateTypeId { get; set; }
        string CertificateType { get; set; }
        DateTime IssueDate { get; set; }
        string CertificateNumber { get; set; }
        IDocument Document { get; }
        void LoadDocument(Stream file);
    }
}
