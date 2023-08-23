using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Scoring
{
    public interface IIsExamSessionLockedCommandDal
    {
        IsExamSessionLockedCommandDto IsExamSessionLocked(int examCaseId);
    }
}
