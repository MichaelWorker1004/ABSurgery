using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IResidencyProgramReadOnlyListFactory
    {
        Task<IResidencyProgramReadOnlyList> GetAllAsync();
    }
}
