using System;

namespace SurgeonPortal.Models.Examinations
{
    public class ExamOverviewReadOnlyModel
    {
        public string ExamName { get; set; }
        public DateTime? RegOpenDate { get; set; }
        public DateTime? RegEndDate { get; set; }
        public DateTime? ExamStartDate { get; set; }
        public DateTime? ExamEndDate { get; set; }
    }
}
