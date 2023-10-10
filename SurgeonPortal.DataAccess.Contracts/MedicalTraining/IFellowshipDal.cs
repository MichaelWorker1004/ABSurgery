using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.MedicalTraining
{
    public interface IFellowshipDal
    {
        Task DeleteAsync(FellowshipDto dto);
        Task<FellowshipDto> GetByIdAsync(System.Collections.Generic.List`1[System.String]);
        Task<FellowshipDto> InsertAsync(FellowshipDto dto);
        Task<FellowshipDto> UpdateAsync(FellowshipDto dto);
    }
}
