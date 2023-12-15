using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.ContinuousCertification
{
    public interface IReferenceLetterReadOnlyListFactory
    {
        Task<IReferenceLetterReadOnlyList> GetAllByUserIdAsync();
    }
}
