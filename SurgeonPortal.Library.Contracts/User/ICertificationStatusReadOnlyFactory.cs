using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.User
{
    public interface ICertificationStatusReadOnlyFactory
    {
        Task<ICertificationStatusReadOnly> GetByUserIdAsync();
    }
}
