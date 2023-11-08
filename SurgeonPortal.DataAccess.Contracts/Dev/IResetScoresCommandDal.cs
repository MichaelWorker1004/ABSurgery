using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Dev
{
    public interface IResetScoresCommandDal
    {
        Task ResetExamScoresAsync(int examinerUserId);
    }
}
