using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Examiners
{
    public interface IAgendaReadOnlyFactory
    {
        Task<IAgendaReadOnly> GetByExamHeaderIdAsync(System.Collections.Generic.List`1[System.String]);
    }
}
