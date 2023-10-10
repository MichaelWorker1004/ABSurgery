using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Dev
{
    public interface IResetScoresCommandFactory
    {
        Task<IResetScoresCommand> ResetExamScoresAsync(System.Collections.Generic.List`1[System.String]);
    }
}
