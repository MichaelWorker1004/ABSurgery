using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IAccreditedProgramInstitutionReadOnly : IYtgReadOnlyBase
    {
        int ProgramId { get; }
        string InstitutionName { get; }
        string City { get; }
        string State { get; }
    }
}
