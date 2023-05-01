using System;
using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.Examinations.GQ
{
    public class AdditionalTrainingModel
    {
        public int TrainingId { get; set; }
        public DateTime DateEnded { get; set; }
        public DateTime DateStarted { get; set; }
        public string Other { get; set; }
        public int InstitutionId { get; set; }
        public string InstitutionName { get; set; }
        public string City { get; set; }
        public string StateId { get; set; }
        public string State { get; set; }
        public string TypeOfTraining { get; set; }
    }
}
