using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IAdmissionCardAvailabilityReadOnlyFactory
    {
        Task<IAdmissionCardAvailabilityReadOnly> GetByExamIdAsync(int examID);
    }
}
