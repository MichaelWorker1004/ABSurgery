using Ytg.Framework.Csla;

namespace SurgeonPortal.DataAccess.Contracts.Scoring
{
    public class CaseScoreDto : YtgBusinessBaseDto
    {
        public int ExamScoringId { get; set; }
        public int ExamCaseId { get; set; }
        public int ExaminerUserId { get; set; }
        public int ExamineeUserId { get; set; }
        public string ExamineeFirstName { get; set; }
        public string ExamineeMiddleName { get; set; }
        public string ExamineeLastName { get; set; }
        public string ExamineeSuffix { get; set; }
        public int Score { get; set; }
        public bool? CriticalFail { get; set; }
        public string Remarks { get; set; }
    }
}
