using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IReferenceLetterTypeReadOnlyListFactory
    {
        Task<IReferenceLetterTypeReadOnlyList> GetAllAsync();
    }
}
