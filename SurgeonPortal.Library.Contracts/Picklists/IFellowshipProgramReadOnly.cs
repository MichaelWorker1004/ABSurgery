using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IFellowshipProgramReadOnly : IYtgReadOnlyBase
    {
        int ProgramId { get; }
        string ProgramName { get; }
    }
}
