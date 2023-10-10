using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.GraduateMedicalEducation
{
    public interface IRotationFactory
    {
        Task<IRotation> GetByIdAsync(System.Collections.Generic.List`1[System.String]);
        IRotation Create();
    }
}
