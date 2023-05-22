using System;
using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.GraduateMedicalEducation
{
    public class RotationModel
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
        public int CreatedByUserId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime LastUpdatedAtUtc { get; set; }
        public int LastUpdatedByUserId { get; set; }
    }
}
