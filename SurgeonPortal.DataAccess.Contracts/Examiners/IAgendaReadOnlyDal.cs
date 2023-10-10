using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.Examiners
{
    public interface IAgendaReadOnlyDal
    {
        Task<AgendaReadOnlyDto> GetByExamHeaderIdAsync(System.Collections.Generic.List`1[System.String]);
    }
}
