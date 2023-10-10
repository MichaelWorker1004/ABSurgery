using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.CE
{
    public interface IGetExamCasesScoredCommandFactory
    {
        IGetExamCasesScoredCommand GetExamCasesScored(System.Collections.Generic.List`1[System.String]);
    }
}
