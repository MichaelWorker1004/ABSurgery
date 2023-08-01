using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Trainees
{
    public interface IRegistrationStatusReadOnlyDal
    {
        Task<RegistrationStatusReadOnlyDto> GetByExamCodeAsync(string examCode);
    }
}
