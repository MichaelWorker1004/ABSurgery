using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Examinations.GQ
{
    public interface IAdditionalTraining : IYtgBusinessBase
    {
        int TrainingId { get; set; }
        DateTime DateEnded { get; set; }
        DateTime DateStarted { get; set; }
        string Other { get; set; }
        int InstitutionId { get; set; }
        string InstitutionName { get; set; }
        string City { get; set; }
        string StateId { get; set; }
        string State { get; set; }
        string TypeOfTraining { get; set; }
    }
}
