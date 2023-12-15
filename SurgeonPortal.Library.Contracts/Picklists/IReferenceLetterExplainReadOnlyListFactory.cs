using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IReferenceLetterExplainReadOnlyListFactory
    {
        Task<IReferenceLetterExplainReadOnlyList> GetAllAsync();
    }
}
