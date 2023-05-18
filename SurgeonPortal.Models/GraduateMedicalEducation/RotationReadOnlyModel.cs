using System;

namespace SurgeonPortal.Models.GraduateMedicalEducation
{
    public class RotationReadOnlyModel
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ProgramName { get; set; }
        public string AlternateInstitutionName { get; set; }
        public string ClinicalLevel { get; set; }
        public string Other { get; set; }
        public string NonSurgicalActivity { get; set; }
        public bool IsInternationalRotation { get; set; }
    }
}
