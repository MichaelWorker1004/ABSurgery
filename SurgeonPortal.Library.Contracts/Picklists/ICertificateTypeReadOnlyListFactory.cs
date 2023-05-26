using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface ICertificateTypeReadOnlyListFactory
    {
        Task<ICertificateTypeReadOnlyList> GetAllAsync();
    }
}
