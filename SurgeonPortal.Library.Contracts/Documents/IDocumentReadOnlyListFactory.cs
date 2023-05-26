using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Documents
{
    public interface IDocumentReadOnlyListFactory
    {
        Task<IDocumentReadOnlyList> GetByUserIdAsync();
    }
}
