using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.ContinuingMedicalEducation
{
    public interface ICmeAdjustmentReadOnlyListFactory
    {
        Task<ICmeAdjustmentReadOnlyList> GetByUserIdAsync();
    }
}
