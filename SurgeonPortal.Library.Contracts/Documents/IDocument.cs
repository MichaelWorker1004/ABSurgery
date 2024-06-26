using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Documents
{
    public interface IDocument : IYtgBusinessBase
    {
        int Id { get;  }
        int UserId { get;  }
        Guid StreamId { get;  }
        int DocumentTypeId { get; set; }
        string DocumentName { get; set; }
        string DocumentType { get; set; }
        bool InternalViewOnly { get; set; }
        string UploadedBy { get; set; }
        DateTime UploadedDateUtc { get; set; }
        System.IO.Stream File { get; set; }
    }
}
