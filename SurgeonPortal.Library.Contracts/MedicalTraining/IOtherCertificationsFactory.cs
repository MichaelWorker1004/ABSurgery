using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.MedicalTraining
{
    public interface IOtherCertificationsFactory
    {
        Task<IOtherCertifications> GetByIdAsync(int id);
        IOtherCertifications Create();
    }
}
