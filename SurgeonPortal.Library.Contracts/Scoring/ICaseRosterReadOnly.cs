using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface ICaseRosterReadOnly : IYtgReadOnlyBase
    {
        string CaseNumber { get; }
        string Description { get; }
        int Id { get; }
    }
}
