using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface ILanguageReadOnlyListFactory
    {
        Task<ILanguageReadOnlyList> GetAllAsync();
    }
}
