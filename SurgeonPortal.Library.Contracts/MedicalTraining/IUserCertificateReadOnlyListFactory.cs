using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.MedicalTraining
{
    public interface IUserCertificateReadOnlyListFactory
    {
        Task<IUserCertificateReadOnlyList> GetByUserAndTypeAsync(int certificateTypeId);
        Task<IUserCertificateReadOnlyList> GetByUserIdAsync();
    }
}
