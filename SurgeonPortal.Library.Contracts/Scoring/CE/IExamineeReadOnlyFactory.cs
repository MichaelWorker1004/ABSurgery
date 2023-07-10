using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Scoring.CE
{
    public interface IExamineeReadOnlyFactory
    {
        Task<IExamineeReadOnly> GetByIdAsync(int examScheduleId);
    }
}
