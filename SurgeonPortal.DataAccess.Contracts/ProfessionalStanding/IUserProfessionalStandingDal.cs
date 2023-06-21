using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.ProfessionalStanding
{
    public interface IUserProfessionalStandingDal
    {
        Task<UserProfessionalStandingDto> GetByUserIdAsync();
        Task<UserProfessionalStandingDto> InsertAsync(UserProfessionalStandingDto dto);
        Task<UserProfessionalStandingDto> UpdateAsync(UserProfessionalStandingDto dto);
    }
}
