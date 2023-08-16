using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IQualifyingExamReadOnlyFactory
    {
        Task<IQualifyingExamReadOnly> GetAsync();
    }
}
