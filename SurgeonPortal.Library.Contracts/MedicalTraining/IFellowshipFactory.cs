using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.MedicalTraining
{
    public interface IFellowshipFactory
    {
        Task<IFellowship> GetByIdAsync(int id);
        IFellowship Create();
    }
}
