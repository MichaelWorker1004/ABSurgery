using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Examinations
{
    public interface IInsertUserExamCommandDal
    {
        Task InsertUserExamAsync(
            int userId,
            int examHeaderId);
    }
}
