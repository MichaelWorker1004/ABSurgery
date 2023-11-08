using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Examinations.GQ
{
    public interface IAdditionalTrainingReadOnlyDal
    {
        Task<IEnumerable<AdditionalTrainingReadOnlyDto>> GetAllByUserIdAsync(int userId);
    }
}
