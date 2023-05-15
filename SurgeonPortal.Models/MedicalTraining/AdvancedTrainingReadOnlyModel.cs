using System;

namespace SurgeonPortal.Models.MedicalTraining
{
    public class AdvancedTrainingReadOnlyModel
    {
        public int Id { get; set; }
        public string TrainingType { get; set; }
        public string InstitutionName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Other { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
