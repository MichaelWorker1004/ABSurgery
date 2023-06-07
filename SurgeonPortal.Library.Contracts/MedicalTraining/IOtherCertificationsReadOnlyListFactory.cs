using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.MedicalTraining
{
    public interface IOtherCertificationsReadOnlyListFactory
    {
        Task<IOtherCertificationsReadOnlyList> GetByUserIdAsync();
    }
}
