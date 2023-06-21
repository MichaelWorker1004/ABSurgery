using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface ILicenseTypeReadOnlyListFactory
    {
        Task<ILicenseTypeReadOnlyList> GetAllAsync();
    }
}
