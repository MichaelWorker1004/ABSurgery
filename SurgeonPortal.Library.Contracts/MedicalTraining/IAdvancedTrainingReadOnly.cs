using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.MedicalTraining
{
    public interface IAdvancedTrainingReadOnly : IYtgReadOnlyBase
    {
        int Id { get; }
        string TrainingType { get; }
        string InstitutionName { get; }
        string City { get; }
        string State { get; }
        string Other { get; }
        DateTime StartDate { get; }
        DateTime EndDate { get; }
    }
}
