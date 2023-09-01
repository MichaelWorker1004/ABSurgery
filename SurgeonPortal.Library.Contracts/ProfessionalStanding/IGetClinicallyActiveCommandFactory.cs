using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.ProfessionalStanding
{
    public interface IGetClinicallyActiveCommandFactory
    {
        IGetClinicallyActiveCommand GetClinicallyActiveByUserId();
    }
}
