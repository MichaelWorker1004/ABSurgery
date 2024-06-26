using System;

namespace SurgeonPortal.DataAccess.Contracts.Scoring
{
    public class CaseScoreReadOnlyDto
    {
        public int? ExamScoringId { get; set; }
        public int ExamCaseId { get; set; }
        public string CaseNumber { get; set; }
        public int? ExaminerUserId { get; set; }
        public int? ExamineeUserId { get; set; }
        public string ExamineeFirstName { get; set; }
        public string ExamineeMiddleName { get; set; }
        public string ExamineeLastName { get; set; }
        public string ExamineeSuffix { get; set; }
        public DateTime? ExamDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int? Score { get; set; }
        public bool? CriticalFail { get; set; }
        public string Remarks { get; set; }
        public bool? IsLocked { get; set; }
    }
}
