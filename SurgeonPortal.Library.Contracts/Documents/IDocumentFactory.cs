using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Documents
{
    public interface IDocumentFactory
    {
        Task<IDocument> GetByIdAsync(System.Collections.Generic.List`1[System.String]);
        IDocument Create();
    }
}
