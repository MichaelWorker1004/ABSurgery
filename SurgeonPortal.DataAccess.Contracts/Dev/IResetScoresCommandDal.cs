using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Dev
{
    public interface IResetScoresCommandDal
    {
        Task ResetExamScoresAsync(System.Collections.Generic.List`1[System.String]);
    }
}
