using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.ProfessionalStanding
{
    public interface IUserProfessionalStandingDal
    {
        Task<UserProfessionalStandingDto> GetByUserIdAsync(int userId);
        Task<UserProfessionalStandingDto> InsertAsync(UserProfessionalStandingDto dto);
        Task<UserProfessionalStandingDto> UpdateAsync(UserProfessionalStandingDto dto);
    }
}
