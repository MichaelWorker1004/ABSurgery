using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Examinations
{
    public interface IExamTitleReadOnlyDal
    {
        Task<ExamTitleReadOnlyDto> GetByExamIdAsync(System.Collections.Generic.List`1[System.String]);
    }
}
