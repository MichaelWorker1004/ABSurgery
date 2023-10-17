using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Examiners
{
    public interface IAgendaReadOnlyFactory
    {
        Task<IAgendaReadOnly> GetByExamHeaderIdAsync(int examHeaderId);
    }
}
