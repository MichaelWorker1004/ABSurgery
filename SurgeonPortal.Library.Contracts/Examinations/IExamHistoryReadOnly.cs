using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IExamHistoryReadOnly : IYtgReadOnlyBase<int>
    {
        int? UserId { get; }
        decimal ExaminationId { get; }
        string ExaminationName { get; }
        DateTime? ExaminationDate { get; }
        int? DocumentId { get; }
        string status { get; }
        string result { get; }
    }
}
