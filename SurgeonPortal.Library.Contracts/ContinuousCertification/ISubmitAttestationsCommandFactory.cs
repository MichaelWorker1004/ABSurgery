using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.ContinuousCertification
{
    public interface ISubmitAttestationsCommandFactory
    {
        Task<ISubmitAttestationsCommand> GetExamCasesScoredAsync(
            System.DateTime sigReceive,
            System.DateTime certnoticeReceive);
    }
}
