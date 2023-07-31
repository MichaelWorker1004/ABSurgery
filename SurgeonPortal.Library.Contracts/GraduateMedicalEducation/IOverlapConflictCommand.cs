using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.GraduateMedicalEducation
{
    public interface IOverlapConflictCommand : IYtgCommandBase
    {
        int UserId { get; }
        DateTime StartDate { get; }
        DateTime EndDate { get; }
        bool OverlapConflict { get; }
        int? RotationId { get; }
    }
}
