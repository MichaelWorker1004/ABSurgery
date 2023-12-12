using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.ContinuousCertification
{
    public interface IReferenceLetterDal
    {
        Task<ReferenceLetterDto> GetByIdAsync(int id);
        Task<ReferenceLetterDto> InsertAsync(ReferenceLetterDto dto);
    }
}
