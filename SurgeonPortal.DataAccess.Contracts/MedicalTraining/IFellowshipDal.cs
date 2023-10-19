using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.MedicalTraining
{
    public interface IFellowshipDal
    {
        Task DeleteAsync(FellowshipDto dto);
        Task<FellowshipDto> GetByIdAsync(int id);
        Task<FellowshipDto> InsertAsync(FellowshipDto dto);
        Task<FellowshipDto> UpdateAsync(FellowshipDto dto);
    }
}
