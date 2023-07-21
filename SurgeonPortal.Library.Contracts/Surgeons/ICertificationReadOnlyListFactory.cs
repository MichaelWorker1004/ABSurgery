using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Surgeons
{
    public interface ICertificationReadOnlyListFactory
    {
        Task<ICertificationReadOnlyList> GetByUserIdAsync();
    }
}
