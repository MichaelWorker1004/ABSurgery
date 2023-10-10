using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Dev
{
    public interface IResetCaseCommentsCommandFactory
    {
        Task<IResetCaseCommentsCommand> ResetCaseCommentsAsync(System.Collections.Generic.List`1[System.String]);
    }
}
