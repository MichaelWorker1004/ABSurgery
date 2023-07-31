using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.GraduateMedicalEducation
{
    public interface IRotationGapReadOnlyListFactory
    {
        Task<IRotationGapReadOnlyList> GetByUserIdAsync();
    }
}
