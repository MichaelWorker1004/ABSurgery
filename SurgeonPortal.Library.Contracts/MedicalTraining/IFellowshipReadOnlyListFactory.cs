using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.MedicalTraining
{
    public interface IFellowshipReadOnlyListFactory
    {
        Task<IFellowshipReadOnlyList> GetByUserIdAsync();
    }
}
