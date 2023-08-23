using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface IIsExamSessionLockedCommandFactory
    {
        IIsExamSessionLockedCommand IsExamSessionLocked(int examCaseId);
    }
}
