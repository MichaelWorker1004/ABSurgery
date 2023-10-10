using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Documents
{
    public interface IDocumentDal
    {
        Task DeleteAsync(DocumentDto dto);
        Task<DocumentDto> GetByIdAsync(System.Collections.Generic.List`1[System.String]);
        Task<DocumentDto> InsertAsync(DocumentDto dto);
    }
}
