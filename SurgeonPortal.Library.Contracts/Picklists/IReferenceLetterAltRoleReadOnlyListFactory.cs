using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IReferenceLetterAltRoleReadOnlyListFactory
    {
        Task<IReferenceLetterAltRoleReadOnlyList> GetAllAsync();
    }
}
