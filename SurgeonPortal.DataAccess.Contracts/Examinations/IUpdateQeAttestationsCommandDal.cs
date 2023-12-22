using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Examinations
{
    public interface IUpdateQeAttestationsCommandDal
    {
        Task UpdateQeAttestationsAsync(
            int userId,
            int examId);
    }
}
