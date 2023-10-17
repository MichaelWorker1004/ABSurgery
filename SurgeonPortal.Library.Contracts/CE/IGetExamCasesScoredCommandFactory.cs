using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.CE
{
    public interface IGetExamCasesScoredCommandFactory
    {
        IGetExamCasesScoredCommand GetExamCasesScored(int examScheduleId);
    }
}
