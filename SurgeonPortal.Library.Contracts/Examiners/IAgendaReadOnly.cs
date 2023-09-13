using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Examiners
{
    public interface IAgendaReadOnly : IYtgReadOnlyBase
    {
        int Id { get; }
        string DocumentName { get; }
    }
}
