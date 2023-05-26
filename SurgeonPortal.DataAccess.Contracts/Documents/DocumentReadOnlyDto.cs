using System;

namespace SurgeonPortal.DataAccess.Contracts.Documents
{
    public class DocumentReadOnlyDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Guid StreamId { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public bool InternalViewOnly { get; set; }
        public string UploadedBy { get; set; }
        public DateTime UploadedDateUtc { get; set; }
    }
}
