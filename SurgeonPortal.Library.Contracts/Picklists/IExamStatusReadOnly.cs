using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IExamStatusReadOnly : IYtgReadOnlyBase
    {
        int Id { get; }
        string Name { get; }
    }
}
