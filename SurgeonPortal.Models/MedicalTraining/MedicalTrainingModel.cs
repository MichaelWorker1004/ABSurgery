using System;
using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.MedicalTraining
{
    public class MedicalTrainingModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GraduateProfileId { get; set; }
        public string GraduateProfileDescription { get; set; }
        public string MedicalSchoolName { get; set; }
        public string MedicalSchoolCity { get; set; }
        public string MedicalSchoolStateId { get; set; }
        public string MedicalSchoolStateName { get; set; }
        public string MedicalSchoolCountryId { get; set; }
        public string MedicalSchoolCountryName { get; set; }
        public int DegreeId { get; set; }
        public string DegreeName { get; set; }
        [RegularExpression(@"^\d{4}$", ErrorMessage = "The medical school completion year doesn't match the required pattern (YYYY)")]
        public string MedicalSchoolCompletionYear { get; set; }
        public string ResidencyProgramName { get; set; }
        [RegularExpression(@"^\d{4}$", ErrorMessage = "The residency completion year doesn't match the required pattern (YYYY)")]
        public string ResidencyCompletionYear { get; set; }
        public string ResidencyProgramOther { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedByUserId { get; set; }
        public int LastUpdatedByUserId { get; set; }
        public DateTime LastUpdatedAtUtc { get; set; }
    }
}
