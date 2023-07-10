using System;
using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface IDashboardRosterReadOnlyListFactory
    {
        Task<IDashboardRosterReadOnlyList> GetByUserIdAsync(DateTime examDate);
    }
}
