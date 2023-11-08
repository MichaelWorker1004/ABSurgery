using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IResidencyProgramReadOnly : IYtgReadOnlyBase<int>
    {
        int ProgramId { get; }
        string ProgramName { get; }
    }
}
