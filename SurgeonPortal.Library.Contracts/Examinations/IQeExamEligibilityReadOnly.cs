using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IQeExamEligibilityReadOnly : IYtgReadOnlyBase<int>
    {
        int? ExamId { get; }
        string ExamCode { get; }
        string ExamName { get; }
        DateTime? AppOpenDate { get; }
        DateTime? AppCloseDate { get; }
        DateTime? AppLateDate { get; }
        DateTime? AppDelayDate { get; }
        DateTime? ExamStartDate { get; }
        DateTime? ExamEndDate { get; }
        int ApplicationApproved { get; }
        int RegistrationOpen { get; }
    }
}
