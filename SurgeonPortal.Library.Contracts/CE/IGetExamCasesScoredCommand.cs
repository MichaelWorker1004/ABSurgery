using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.CE
{
    public interface IGetExamCasesScoredCommand : IYtgCommandBase<int>
    {
        int ExamScheduleId { get; }
        bool CasesScored { get; }
    }
}
