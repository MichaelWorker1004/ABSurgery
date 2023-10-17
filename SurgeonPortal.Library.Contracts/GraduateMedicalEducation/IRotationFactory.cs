using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.GraduateMedicalEducation
{
    public interface IRotationFactory
    {
        Task<IRotation> GetByIdAsync(int id);
        IRotation Create();
    }
}
