using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.ContinuousCertification
{
    public interface IReferenceLetterFactory
    {
        Task<IReferenceLetter> GetByIdAsync(int id);
        IReferenceLetter Create();
    }
}
