using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IAdmissionCardAvailabilityReadOnly : IYtgReadOnlyBase<int>
    {
        bool AdmCardAvailable { get; }
        DateTime? DatePosted { get; }
        string ExamCode { get; }
    }
}
