using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Users
{
    public interface IUserTokenDal
    {
        Task<UserTokenDto> GetActiveAsync(string token);
        Task<UserTokenDto> InsertAsync(UserTokenDto dto);
    }
}
