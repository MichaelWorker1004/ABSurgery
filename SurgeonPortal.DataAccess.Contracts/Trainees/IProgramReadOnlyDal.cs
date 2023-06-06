using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Trainees
{
    public interface IProgramReadOnlyDal
    {
        Task<ProgramReadOnlyDto> GetByUserIdAsync();
    }
}
