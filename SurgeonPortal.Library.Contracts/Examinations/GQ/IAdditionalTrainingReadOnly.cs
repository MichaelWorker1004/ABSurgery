using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Examinations.GQ
{
    public interface IAdditionalTrainingReadOnly : IYtgReadOnlyBase<int>
    {
        int TrainingId { get; }
        string TypeOfTraining { get; }
        string State { get; }
        string City { get; }
        string InstitutionName { get; }
        string Other { get; }
        DateTime DateStarted { get; }
        DateTime DateEnded { get; }
    }
}
