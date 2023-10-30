using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Scoring.CE
{
    public interface ITitleReadOnly : IYtgReadOnlyBase
    {
        string Title { get; }
        int CaseHeaderId { get; }
        int? ExamCaseId { get; }
        ICaseDetailReadOnlyList Sections { get; }
        int? Score { get; }
        bool? CriticalFail { get; }
        string Remarks { get; }
    }
}
