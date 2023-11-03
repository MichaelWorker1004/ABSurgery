using System;
using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.Documents
{
    public class DocumentModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Guid StreamId { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public bool InternalViewOnly { get; set; }
        public int CreatedByUserId { get; set; }
        public string UploadedBy { get; set; }
        public DateTime UploadedDateUtc { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime LastUpdatedAtUtc { get; set; }
        public int LastUpdatedByUserId { get; set; }
    }
}
