using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Users
{
    public interface IUserCredentialDal
    {
        Task<UserCredentialDto> GetByUserIdAsync();
        Task<UserCredentialDto> UpdateAsync(UserCredentialDto dto);
    }
}
