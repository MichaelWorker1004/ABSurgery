using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IExamIntentions : IYtgBusinessBase
    {
        int UserId { get; set; }
        int ExamId { get; set; }
        string ExamName { get; set; }
        DateTime? ExamDate { get; set; }
        DateTime? DateReceived { get; set; }
        bool Intention { get; set; }
    }
}
