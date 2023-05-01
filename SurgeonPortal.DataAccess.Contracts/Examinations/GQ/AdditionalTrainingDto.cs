using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.DataAccess.Contracts.Examinations.GQ
{
    public class AdditionalTrainingDto : YtgBusinessBaseDto
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
