using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IAccreditedProgramInstitutionReadOnlyListFactory
    {
        Task<IAccreditedProgramInstitutionReadOnlyList> GetAllAsync();
    }
}
