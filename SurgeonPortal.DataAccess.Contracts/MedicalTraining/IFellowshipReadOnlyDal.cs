using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.MedicalTraining
{
    public interface IFellowshipReadOnlyDal
    {
        Task<IEnumerable<FellowshipReadOnlyDto>> GetByUserIdAsync();
    }
}
