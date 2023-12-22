using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IUpdateQeAttestationsCommandFactory
    {
        Task<IUpdateQeAttestationsCommand> UpdateQeAttestationsAsync(int examId);
    }
}
