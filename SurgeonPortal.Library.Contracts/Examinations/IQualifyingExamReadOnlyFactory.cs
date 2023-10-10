using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IQualifyingExamReadOnlyFactory
    {
        Task<IQualifyingExamReadOnly> GetAsync(System.Collections.Generic.List`1[System.String]);
    }
}
