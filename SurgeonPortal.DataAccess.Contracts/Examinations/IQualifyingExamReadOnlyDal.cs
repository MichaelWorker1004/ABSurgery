using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Examinations
{
    public interface IQualifyingExamReadOnlyDal
    {
        Task<QualifyingExamReadOnlyDto> GetAsync();
    }
}
