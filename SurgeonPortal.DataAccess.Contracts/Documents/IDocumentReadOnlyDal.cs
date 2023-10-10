using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Documents
{
    public interface IDocumentReadOnlyDal
    {
        Task<IEnumerable<DocumentReadOnlyDto>> GetByUserIdAsync(System.Collections.Generic.List`1[System.String]);
    }
}
