using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Surgeons
{
    public interface ICertificationReadOnlyFactory
    {
        Task<ICertificationReadOnly> GetByAbsIdAsync(string absId);
    }
}
