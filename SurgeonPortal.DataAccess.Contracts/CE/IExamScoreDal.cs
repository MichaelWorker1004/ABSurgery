using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.CE
{
    public interface IExamScoreDal
    {
        Task<ExamScoreDto> GetByIdAsync(int examScheduleScoreId);
        Task<ExamScoreDto> InsertAsync(ExamScoreDto dto);
        Task<ExamScoreDto> UpdateAsync(ExamScoreDto dto);
    }
}
