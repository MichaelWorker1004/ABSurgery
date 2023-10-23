using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.GraduateMedicalEducation
{
    public interface IRotationGapReadOnly : IYtgReadOnlyBase<int>
    {
        DateTime? StartDate { get; }
        DateTime? EndDate { get; }
        int PreviousRotationId { get; }
        int? NextRotationId { get; }
    }
}
