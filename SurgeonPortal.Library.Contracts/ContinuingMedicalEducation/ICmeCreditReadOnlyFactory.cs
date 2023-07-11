using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.ContinuingMedicalEducation
{
    public interface ICmeCreditReadOnlyFactory
    {
        Task<ICmeCreditReadOnly> GetByIdAsync(int cmeId);
    }
}
