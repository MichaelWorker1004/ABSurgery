using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.ContinuingMedicalEducation
{
    public interface ICmeCreditReadOnlyFactory
    {
        Task<ICmeCreditReadOnly> GetByIdAsync(System.Collections.Generic.List`1[System.String]);
    }
}
