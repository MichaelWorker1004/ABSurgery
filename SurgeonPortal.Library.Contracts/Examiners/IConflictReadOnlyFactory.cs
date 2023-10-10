using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Examiners
{
    public interface IConflictReadOnlyFactory
    {
        Task<IConflictReadOnly> GetByExamHeaderIdAsync(System.Collections.Generic.List`1[System.String]);
    }
}
