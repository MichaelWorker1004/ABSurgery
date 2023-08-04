using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.GraduateMedicalEducation
{
    public interface IRotation : IYtgBusinessBase
    {
        int Id { get; set; }
        int UserId { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        int ClinicalLevelId { get; set; }
        string ClinicalLevel { get; set; }
        int ClinicalActivityId { get; set; }
        string ProgramName { get; set; }
        string NonSurgicalActivity { get; set; }
        string AlternateInstitutionName { get; set; }
        bool IsInternationalRotation { get; set; }
        bool IsEssential { get; set; }
        bool IsCredit { get; set; }
        string Other { get; set; }
        string FourMonthRotationExplain { get; set; }
        string NonPrimaryExplain { get; set; }
        string NonClinicalExplain { get; set; }
        string ClinicalActivity { get; set; }
    }
}
