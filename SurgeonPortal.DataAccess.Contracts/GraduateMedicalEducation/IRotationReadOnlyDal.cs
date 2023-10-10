using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.GraduateMedicalEducation
{
    public interface IRotationReadOnlyDal
    {
        Task<IEnumerable<RotationReadOnlyDto>> GetByUserIdAsync(System.Collections.Generic.List`1[System.String]);
    }
}
