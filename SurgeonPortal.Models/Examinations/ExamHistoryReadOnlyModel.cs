using System;

namespace SurgeonPortal.Models.Examinations
{
    public class ExamHistoryReadOnlyModel
    {
        public int? UserId { get; set; }
        public decimal ExaminationId { get; set; }
        public string ExaminationName { get; set; }
        public DateTime? ExaminationDate { get; set; }
        public int? DocumentId { get; set; }
        public string status { get; set; }
        public string result { get; set; }
    }
}
