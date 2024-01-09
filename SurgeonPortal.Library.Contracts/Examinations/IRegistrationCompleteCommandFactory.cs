using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IRegistrationCompleteCommandFactory
    {
        Task<IRegistrationCompleteCommand> CompleteRegistrationAsync(int examHeaderId);
    }
}
