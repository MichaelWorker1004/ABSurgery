using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IExamTitleReadOnly : IYtgReadOnlyBase<int>
    {
        string ExamName { get; }
    }
}
