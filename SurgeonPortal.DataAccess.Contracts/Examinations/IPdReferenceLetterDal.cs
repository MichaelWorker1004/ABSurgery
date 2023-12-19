using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Examinations
{
    public interface IPdReferenceLetterDal
    {
        Task<PdReferenceLetterDto> GetByExamIdAsync(
            int userId,
            int examId);
        Task<PdReferenceLetterDto> InsertAsync(PdReferenceLetterDto dto);
    }
}
