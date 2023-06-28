using System;
using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Scoring
{
    public interface IExamSessionReadOnlyListFactory
    {
        Task<IExamSessionReadOnlyList> GetByUserIdAsync(DateTime examDate);
    }
}
