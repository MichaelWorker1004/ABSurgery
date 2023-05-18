using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.GraduateMedicalEducation
{
    public interface IRotationReadOnlyListFactory
    {
        Task<IRotationReadOnlyList> GetByUserIdAsync();
    }
}
