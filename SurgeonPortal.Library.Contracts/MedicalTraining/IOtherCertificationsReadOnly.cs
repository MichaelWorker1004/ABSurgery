using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.MedicalTraining
{
    public interface IOtherCertificationsReadOnly : IYtgReadOnlyBase
    {
        int Id { get; }
        int UserId { get; }
        int CertificateTypeId { get; }
        string CertificateTypeName { get; }
        DateTime IssueDate { get; }
        string CertificateNumber { get; }
    }
}
