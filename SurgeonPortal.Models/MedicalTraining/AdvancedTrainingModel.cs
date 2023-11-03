using System;
using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.MedicalTraining
{
    public class AdvancedTrainingModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = "TrainingTypeId is required")]
        public int? TrainingTypeId { get; set; }
        public string TrainingType { get; set; }
        public int? ProgramId { get; set; }
        public string InstitutionName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Other { get; set; }
        [Required(ErrorMessage = "StartDate is required")]
        public DateTime? StartDate { get; set; }
        [Required(ErrorMessage = "EndDate is required")]
        public DateTime? EndDate { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime LastUpdatedAtUtc { get; set; }
        public int LastUpdatedByUserId { get; set; }
    }
}
