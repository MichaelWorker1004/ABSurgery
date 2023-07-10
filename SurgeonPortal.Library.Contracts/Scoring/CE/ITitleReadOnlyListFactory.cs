using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Scoring.CE
{
    public interface ITitleReadOnlyListFactory
    {
        Task<ITitleReadOnlyList> GetByIdAsync(int examScheduleId);
    }
}
