using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Examinations
{
    public interface IQualifyingExamReadOnlyDal
    {
        Task<QualifyingExamReadOnlyDto> GetAsync(System.Collections.Generic.List`1[System.String]);
    }
}
