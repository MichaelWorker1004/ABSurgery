using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Scoring.CE
{
    public interface IExamineeReadOnlyDal
    {
        Task<ExamineeReadOnlyDto> GetByIdAsync(int examScheduleId);
    }
}
