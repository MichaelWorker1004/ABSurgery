using System;

namespace SurgeonPortal.DataAccess.Contracts.Examinations
{
    public class QualifyingExamReadOnlyDto
    {
        public string ExamName { get; set; }
        public DateTime? RegOpenDate { get; set; }
        public DateTime? RegEndDate { get; set; }
        public DateTime? ExamStartDate { get; set; }
        public DateTime? ExamEndDate { get; set; }
        public int ExamId { get; set; }
    }
}
