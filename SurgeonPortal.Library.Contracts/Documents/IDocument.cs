using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Documents
{
    public interface IDocument : IYtgBusinessBase
    {
        int Id { get; set; }
        int UserId { get; set; }
        Guid StreamId { get; set; }
        int DocumentTypeId { get; set; }
        string DocumentName { get; set; }
        string DocumentType { get; set; }
        bool InternalViewOnly { get; set; }
        string UploadedBy { get; set; }
        DateTime UploadedDateUtc { get; set; }
        System.IO.Stream File { get; }
    }
}
