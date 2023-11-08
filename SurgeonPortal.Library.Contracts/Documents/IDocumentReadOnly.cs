using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Documents
{
    public interface IDocumentReadOnly : IYtgReadOnlyBase<int>
    {
        int Id { get; }
        int UserId { get; }
        Guid StreamId { get; }
        int DocumentTypeId { get; }
        string DocumentName { get; }
        string DocumentType { get; }
        bool InternalViewOnly { get; }
        string UploadedBy { get; }
        DateTime UploadedDateUtc { get; }
    }
}
