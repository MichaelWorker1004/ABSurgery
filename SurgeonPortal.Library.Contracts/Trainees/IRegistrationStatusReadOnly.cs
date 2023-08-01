using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Trainees
{
    public interface IRegistrationStatusReadOnly : IYtgReadOnlyBase
    {
        DateTime? RegOpenDate { get; }
        DateTime? RegEndDate { get; }
        int isRegOpen { get; }
        DateTime? RegLateDate { get; }
        int isRegLate { get; }
    }
}
