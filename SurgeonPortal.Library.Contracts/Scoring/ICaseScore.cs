using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface ICaseScore : IYtgBusinessBase
    {
        int ExamScoringId { get; set; }
        int ExamCaseId { get; set; }
        int ExaminerUserId { get; set; }
        int ExamineeUserId { get; set; }
        string ExamineeFirstName { get; set; }
        string ExamineeMiddleName { get; set; }
        string ExamineeLastName { get; set; }
        string ExamineeSuffix { get; set; }
        int Score { get; set; }
        bool? CriticalFail { get; set; }
        string Remarks { get; set; }
    }
}
