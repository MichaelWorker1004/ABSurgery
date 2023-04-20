using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Surgeons
{
    public interface ICertificationReadOnlyDal
    {
        Task<IEnumerable<CertificationReadOnlyDto>> GetByAbsIdAsync(string absId);
    }
}
