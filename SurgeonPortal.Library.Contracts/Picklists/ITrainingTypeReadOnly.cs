using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface ITrainingTypeReadOnly : IYtgReadOnlyBase
    {
        int Id { get; }
        string TrainingType { get; }
    }
}
