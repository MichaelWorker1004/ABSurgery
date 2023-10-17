using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Dev
{
    public interface IResetScoresCommandFactory
    {
        Task<IResetScoresCommand> ResetExamScoresAsync();
    }
}
