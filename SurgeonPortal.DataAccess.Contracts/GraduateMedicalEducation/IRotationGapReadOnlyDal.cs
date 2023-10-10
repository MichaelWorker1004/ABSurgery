using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.GraduateMedicalEducation
{
    public interface IRotationGapReadOnlyDal
    {
        Task<IEnumerable<RotationGapReadOnlyDto>> GetByUserIdAsync(System.Collections.Generic.List`1[System.String]);
    }
}
