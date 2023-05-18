using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.GraduateMedicalEducation
{
    public interface IRotationReadOnly : IYtgReadOnlyBase
    {
        int Id { get; }
        DateTime StartDate { get; }
        DateTime EndDate { get; }
        string ProgramName { get; }
        string AlternateInstitutionName { get; }
        string ClinicalLevel { get; }
        string Other { get; }
        string NonSurgicalActivity { get; }
        bool IsInternationalRotation { get; }
    }
}
