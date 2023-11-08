using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Dev
{
    public interface IResetCaseCommentsCommandDal
    {
        Task ResetCaseCommentsAsync(int examinerUserId);
    }
}
