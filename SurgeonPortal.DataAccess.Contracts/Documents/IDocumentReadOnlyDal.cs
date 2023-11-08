using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Documents
{
    public interface IDocumentReadOnlyDal
    {
        Task<IEnumerable<DocumentReadOnlyDto>> GetByUserIdAsync(int userId);
    }
}
