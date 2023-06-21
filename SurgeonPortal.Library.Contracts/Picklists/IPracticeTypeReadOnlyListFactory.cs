using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IPracticeTypeReadOnlyListFactory
    {
        Task<IPracticeTypeReadOnlyList> GetAllAsync();
    }
}
