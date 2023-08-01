using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Trainees
{
    public interface IRegistrationStatusReadOnlyFactory
    {
        Task<IRegistrationStatusReadOnly> GetByExamCodeAsync(string examCode);
    }
}
