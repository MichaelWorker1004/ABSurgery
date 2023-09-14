using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Examiners
{
    public interface IAgendaReadOnlyDal
    {
        Task<AgendaReadOnlyDto> GetByExamHeaderIdAsync(int examHeaderId);
    }
}
