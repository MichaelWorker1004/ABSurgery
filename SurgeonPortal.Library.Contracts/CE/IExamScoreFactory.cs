using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.CE
{
    public interface IExamScoreFactory
    {
        Task<IExamScore> GetByIdAsync(System.Collections.Generic.List`1[System.String]);
        IExamScore Create();
    }
}
