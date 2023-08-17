using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IFellowshipTypeReadOnlyFactory
    {
        Task<IFellowshipTypeReadOnly> GetAsync();
    }
}
