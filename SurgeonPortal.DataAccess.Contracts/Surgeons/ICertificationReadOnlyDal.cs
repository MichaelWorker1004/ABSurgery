using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Surgeons
{
    public interface ICertificationReadOnlyDal
    {
        Task<CertificationReadOnlyDto> GetByAbsIdAsync(string absId);
    }
}
