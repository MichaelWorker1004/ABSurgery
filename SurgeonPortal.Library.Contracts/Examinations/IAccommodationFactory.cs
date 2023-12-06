using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IAccommodationFactory
    {
        Task<IAccommodation> GetByExamIdAsync(int examId);
        IAccommodation Create();
    }
}
