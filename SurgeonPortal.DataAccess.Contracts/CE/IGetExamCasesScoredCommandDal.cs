using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.CE
{
    public interface IGetExamCasesScoredCommandDal
    {
        GetExamCasesScoredCommandDto GetExamCasesScored(int examScheduleId);
    }
}
