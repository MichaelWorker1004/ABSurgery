using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IDocumentTypeReadOnlyListFactory
    {
        Task<IDocumentTypeReadOnlyList> GetAllAsync();
    }
}
