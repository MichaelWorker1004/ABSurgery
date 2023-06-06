using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.MedicalTraining
{
    public interface IUserCertificateReadOnlyListFactory
    {
        Task<IUserCertificateReadOnlyList> GetByUserIdAsync();
    }
}
