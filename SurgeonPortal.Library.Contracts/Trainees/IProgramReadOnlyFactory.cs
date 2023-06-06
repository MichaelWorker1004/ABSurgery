using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Trainees
{
    public interface IProgramReadOnlyFactory
    {
        Task<IProgramReadOnly> GetByUserIdAsync();
    }
}
