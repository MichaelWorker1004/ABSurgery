using System;

namespace SurgeonPortal.Models.Examinations
{
    public class ExamDetailReadOnlyModel
    {
        public int? UserId { get; set; }
        public decimal ExaminationId { get; set; }
        public Guid? ReportOfPerformancePdfStreamId { get; set; }
        public decimal? score { get; set; }
    }
}
