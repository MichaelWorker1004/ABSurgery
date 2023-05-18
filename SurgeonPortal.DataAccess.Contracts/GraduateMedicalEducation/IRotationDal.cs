using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.GraduateMedicalEducation
{
    public interface IRotationDal
    {
        Task DeleteAsync(RotationDto dto);
        Task<RotationDto> GetByIdAsync(int id);
        Task<RotationDto> InsertAsync(RotationDto dto);
        Task<RotationDto> UpdateAsync(RotationDto dto);
    }
}
