using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Picklists
{
    public interface ICertificateTypeReadOnlyDal
    {
        Task<IEnumerable<CertificateTypeReadOnlyDto>> GetAllAsync();
    }
}
