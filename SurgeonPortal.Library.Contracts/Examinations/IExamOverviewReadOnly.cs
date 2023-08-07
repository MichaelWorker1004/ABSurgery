using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IExamOverviewReadOnly : IYtgReadOnlyBase
    {
        string ExamName { get; }
        DateTime? RegOpenDate { get; }
        DateTime? RegEndDate { get; }
        DateTime? ExamStartDate { get; }
        DateTime? ExamEndDate { get; }
    }
}
