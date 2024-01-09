using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Examinations
{
    public interface IRegistrationCompleteCommandDal
    {
        Task CompleteRegistrationAsync(
            int userId,
            int examHeaderId);
    }
}
