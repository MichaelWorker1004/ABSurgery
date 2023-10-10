using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Examiners
{
    public interface IConflictReadOnlyDal
    {
        Task<ConflictReadOnlyDto> GetByExamHeaderIdAsync(System.Collections.Generic.List`1[System.String]);
    }
}
