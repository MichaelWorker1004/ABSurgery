using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface ICaseScoreReadOnly : IYtgReadOnlyBase
    {
        int? ExamScoringId { get; }
        int ExamCaseId { get; }
        int? ExaminerUserId { get; }
        int? ExamineeUserId { get; }
        string ExamineeFirstName { get; }
        string ExamineeMiddleName { get; }
        string ExamineeLastName { get; }
        string ExamineeSuffix { get; }
        DateTime? ExamDate { get; }
        TimeSpan StartTime { get; }
        TimeSpan EndTime { get; }
        int? Score { get; }
        bool? CriticalFail { get; }
        string Remarks { get; }
        bool? isLocked { get; }
    }
}
