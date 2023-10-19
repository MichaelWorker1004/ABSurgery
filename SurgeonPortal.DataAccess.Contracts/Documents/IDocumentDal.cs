using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Documents
{
    public interface IDocumentDal
    {
        Task DeleteAsync(DocumentDto dto);
        Task<DocumentDto> GetByIdAsync(int id);
        Task<DocumentDto> InsertAsync(DocumentDto dto);
    }
}
