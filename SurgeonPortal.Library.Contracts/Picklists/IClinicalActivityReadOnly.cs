using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IClinicalActivityReadOnly : IYtgReadOnlyBase
    {
        int Id { get; }
        string Name { get; }
        bool IsCredit { get; }
        bool IsEssential { get; }
    }
}
