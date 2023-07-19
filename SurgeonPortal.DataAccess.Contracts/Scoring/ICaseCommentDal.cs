using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Scoring
{
    public interface ICaseCommentDal
    {
        Task DeleteAsync(CaseCommentDto dto);
        Task<CaseCommentDto> GetByIdAsync(int id);
        Task<CaseCommentDto> InsertAsync(CaseCommentDto dto);
        Task<CaseCommentDto> UpdateAsync(CaseCommentDto dto);
    }
}
