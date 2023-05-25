using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.GraduateMedicalEducation
{
    public interface IGmeSummaryReadOnlyListFactory
    {
        Task<IGmeSummaryReadOnlyList> GetByUserIdAsync();
    }
}
