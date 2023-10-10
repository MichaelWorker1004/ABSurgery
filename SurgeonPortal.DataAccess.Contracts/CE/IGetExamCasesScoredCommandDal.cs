using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.CE
{
    public interface IGetExamCasesScoredCommandDal
    {
        GetExamCasesScoredCommandDto GetExamCasesScored(System.Collections.Generic.List`1[System.String]);
    }
}
