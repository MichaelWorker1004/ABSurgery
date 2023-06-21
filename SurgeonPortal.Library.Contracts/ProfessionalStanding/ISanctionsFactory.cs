using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.ProfessionalStanding
{
    public interface ISanctionsFactory
    {
        Task<ISanctions> GetByUserIdAsync();
        ISanctions Create();
    }
}
