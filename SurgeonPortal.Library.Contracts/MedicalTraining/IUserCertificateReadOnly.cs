using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.MedicalTraining
{
    public interface IUserCertificateReadOnly : IYtgReadOnlyBase
    {
        int CertificateId { get; }
        int UserId { get; }
        int DocumentId { get; }
        int CertificateTypeId { get; }
        string CertificateType { get; }
        DateTime IssueDate { get; }
        string CertificateNumber { get; }
        string DocumentName { get; }
        DateTime UploadDateUtc { get; }
    }
}
