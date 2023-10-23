using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface IDashboardRosterReadOnly : IYtgReadOnlyBase<int>
    {
        string FirstName { get; }
        string MiddleName { get; }
        string LastName { get; }
        int? SessionNumber { get; }
        TimeSpan StartTime { get; }
        TimeSpan EndTime { get; }
    }
}
