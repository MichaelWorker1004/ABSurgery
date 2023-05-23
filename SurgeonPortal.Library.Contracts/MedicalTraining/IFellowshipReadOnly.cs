using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.MedicalTraining
{
    public interface IFellowshipReadOnly : IYtgReadOnlyBase
    {
        int Id { get; }
        string ProgramName { get; }
        string CompletionYear { get; }
        string ProgramOther { get; }
    }
}
