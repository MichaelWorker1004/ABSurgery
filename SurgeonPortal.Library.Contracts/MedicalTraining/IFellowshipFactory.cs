using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.MedicalTraining
{
    public interface IFellowshipFactory
    {
        Task<IFellowship> GetByIdAsync(System.Collections.Generic.List`1[System.String]);
        IFellowship Create();
    }
}
