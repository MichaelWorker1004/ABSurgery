using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IAdmissionCardAvailabilityReadOnly : IYtgReadOnlyBase<int>
    {
        bool AdmCardAvailable { get; }
        int? ExamId { get; }
    }
}
