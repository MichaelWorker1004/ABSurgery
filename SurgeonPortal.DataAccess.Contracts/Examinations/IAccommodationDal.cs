using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Examinations
{
    public interface IAccommodationDal
    {
        Task DeleteAsync(AccommodationDto dto);
        Task<AccommodationDto> GetByExamIdAsync(
            int userId,
            int examId);
        Task<AccommodationDto> InsertAsync(AccommodationDto dto);
        Task<AccommodationDto> UpdateAsync(AccommodationDto dto);
    }
}
