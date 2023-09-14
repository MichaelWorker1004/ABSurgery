using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Billing
{
    public interface IExamFeeReadOnlyListFactory
    {
        Task<IExamFeeReadOnlyList> GetByUserIdAsync();
    }
}
