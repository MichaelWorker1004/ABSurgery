using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.CE
{
    public interface IExamScore : IYtgBusinessBase
    {
        int ExamScheduleScoreId { get; set; }
        int ExamScheduleId { get; set; }
        int ExaminerUserId { get; set; }
        int ExaminerScore { get; set; }
        bool? Submitted { get; set; }
        DateTime? SubmittedDateTimeUTC { get; set; }
    }
}
