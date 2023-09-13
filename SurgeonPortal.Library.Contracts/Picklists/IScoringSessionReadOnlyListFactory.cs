using System;
using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Picklists
{
    public interface IScoringSessionReadOnlyListFactory
    {
        Task<IScoringSessionReadOnlyList> GetByKeysAsync(DateTime currentDate);
    }
}
