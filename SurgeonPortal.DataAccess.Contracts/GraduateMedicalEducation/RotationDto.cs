using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.DataAccess.Contracts.GraduateMedicalEducation
{
    public class RotationDto : YtgBusinessBaseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ClinicalLevelId { get; set; }
        public string ClinicalLevel { get; set; }
        public int ClinicalActivityId { get; set; }
        public string ProgramName { get; set; }
        public string NonSurgicalActivity { get; set; }
        public string AlternateInstitutionName { get; set; }
        public bool IsInternationalRotation { get; set; }
        public bool IsEssential { get; set; }
        public bool IsCredit { get; set; }
        public string Other { get; set; }
        public string FourMonthRotationExplain { get; set; }
        public string NonPrimaryExplain { get; set; }
        public string NonClinicalExplain { get; set; }
        public string ClinicalActivity { get; set; }
    }
}
