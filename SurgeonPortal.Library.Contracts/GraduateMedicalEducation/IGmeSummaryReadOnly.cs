using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.GraduateMedicalEducation
{
    public interface IGmeSummaryReadOnly : IYtgReadOnlyBase<int>
    {
        string ClinicalLevel { get; }
        DateTime? MinStartDate { get; }
        DateTime? MaxStartDate { get; }
        string ProgramName { get; }
        int? ClinicalWeeks { get; }
        int? NonClinicalWeeks { get; }
        int? EssentialsWeeks { get; }
    }
}
