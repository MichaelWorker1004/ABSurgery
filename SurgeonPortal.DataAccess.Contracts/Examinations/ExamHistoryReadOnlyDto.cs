using System;

namespace SurgeonPortal.DataAccess.Contracts.Examinations
{
    public class ExamHistoryReadOnlyDto
    {
        public int? UserId { get; set; }
        public decimal ExaminationId { get; set; }
        public string ExaminationName { get; set; }
        public DateTime? ExaminationDate { get; set; }
        public int? DocumentId { get; set; }
        public string Status { get; set; }
        public string Result { get; set; }
    }
}
