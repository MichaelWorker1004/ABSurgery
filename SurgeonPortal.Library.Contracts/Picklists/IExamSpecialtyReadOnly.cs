using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IExamSpecialtyReadOnly : IYtgReadOnlyBase
    {
        int Id { get; }
        string Name { get; }
    }
}
