using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IInsertUserExamCommandFactory
    {
        Task<IInsertUserExamCommand> InsertUserExamAsync(int examHeaderId);
    }
}
