using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Dev
{
    public interface IResetCaseCommentsCommandDal
    {
        Task ResetCaseCommentsAsync(System.Collections.Generic.List`1[System.String]);
    }
}
