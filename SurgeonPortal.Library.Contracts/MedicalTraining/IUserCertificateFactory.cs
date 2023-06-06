using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.MedicalTraining
{
    public interface IUserCertificateFactory
    {
        Task<IUserCertificate> GetByIdAsync(int id);
        IUserCertificate Create();
    }
}
