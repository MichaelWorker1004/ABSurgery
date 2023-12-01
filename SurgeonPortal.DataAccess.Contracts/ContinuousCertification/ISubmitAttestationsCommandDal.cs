using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.ContinuousCertification
{
    public interface ISubmitAttestationsCommandDal
    {
        Task GetExamCasesScoredAsync(
            int userId,
            System.DateTime sigReceive,
            System.DateTime certnoticeReceive);
    }
}
