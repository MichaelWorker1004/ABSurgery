using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.ProfessionalStanding
{
    public interface IGetClinicallyActiveCommandDal
    {
        GetClinicallyActiveCommandDto GetClinicallyActiveByUserId(int userId);
    }
}
