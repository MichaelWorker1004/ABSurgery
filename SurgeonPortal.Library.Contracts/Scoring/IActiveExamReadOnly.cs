using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface IActiveExamReadOnly : IYtgReadOnlyBase<int>
    {
        int ExamHeaderId { get; }
    }
}
