using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.DataAccess.Contracts.MedicalTraining
{
    public class AdvancedTrainingDto : YtgBusinessBaseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TrainingTypeId { get; set; }
        public string TrainingType { get; set; }
        public int? ProgramId { get; set; }
        public string InstitutionName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Other { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
