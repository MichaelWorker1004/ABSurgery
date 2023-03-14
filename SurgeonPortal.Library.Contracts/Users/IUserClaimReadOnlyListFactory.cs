using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Users
{
    public interface IUserClaimReadOnlyListFactory
    {
        Task<IUserClaimReadOnlyList> GetByIdsAsync(
            int userId,
            int applicationId);
    }
}
