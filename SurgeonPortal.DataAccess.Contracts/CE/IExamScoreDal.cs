using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.CE
{
    public interface IExamScoreDal
    {
        Task<ExamScoreDto> GetByIdAsync(System.Collections.Generic.List`1[System.String]);
        Task<ExamScoreDto> InsertAsync(ExamScoreDto dto);
        Task<ExamScoreDto> UpdateAsync(ExamScoreDto dto);
    }
}
