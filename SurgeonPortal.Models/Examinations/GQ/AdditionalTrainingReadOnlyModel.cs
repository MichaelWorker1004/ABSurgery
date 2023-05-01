using System;

namespace SurgeonPortal.Models.Examinations.GQ
{
    public class AdditionalTrainingReadOnlyModel
    {
        public int TrainingId { get; set; }
        public string TypeOfTraining { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string InstitutionName { get; set; }
        public string Other { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime DateEnded { get; set; }
    }
}
