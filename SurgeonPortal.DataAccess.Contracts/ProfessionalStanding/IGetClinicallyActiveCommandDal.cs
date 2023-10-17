using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.ProfessionalStanding
{
    public interface IGetClinicallyActiveCommandDal
    {
        GetClinicallyActiveCommandDto GetClinicallyActiveByUserId(System.Collections.Generic.List`1[System.String]);
    }
}
