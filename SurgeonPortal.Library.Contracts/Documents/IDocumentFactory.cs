using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Documents
{
    public interface IDocumentFactory
    {
        Task<IDocument> GetByIdAsync(int id);
        IDocument Create();
    }
}
