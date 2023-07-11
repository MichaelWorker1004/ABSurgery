using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.ContinuingMedicalEducation
{
    public interface ICmeCreditReadOnlyListFactory
    {
        Task<ICmeCreditReadOnlyList> GetByUserIdAsync();
    }
}
