using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.ProfessionalStanding
{
    public interface IUserProfessionalStandingFactory
    {
        Task<IUserProfessionalStanding> GetByUserIdAsync();
        IUserProfessionalStanding Create();
    }
}
