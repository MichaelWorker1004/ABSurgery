using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IQeAttestationReadOnlyListFactory
    {
        Task<IQeAttestationReadOnlyList> GetByExamIdAsync(int examId);
    }
}
