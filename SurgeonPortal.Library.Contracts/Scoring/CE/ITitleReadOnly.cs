using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Scoring.CE
{
    public interface ITitleReadOnly : IYtgReadOnlyBase<int>
    {
        string Title { get; }
        int CaseHeaderId { get; }
        int? ExamCaseId { get; }

        ICaseDetailReadOnlyList Sections { get; }
    }
}
