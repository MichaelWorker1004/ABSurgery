using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Documents
{
    public interface IDocumentDal
    {
        Task DeleteAsync(DocumentDto dto);
        Task<DocumentDto> GetByIdAsync(
            int id,
            int userId);
        Task<DocumentDto> InsertAsync(DocumentDto dto);
    }
}
