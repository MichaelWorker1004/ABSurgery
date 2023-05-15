using System;

namespace SurgeonPortal.DataAccess.Contracts.MedicalTraining
{
    public class AdvancedTrainingReadOnlyDto
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
