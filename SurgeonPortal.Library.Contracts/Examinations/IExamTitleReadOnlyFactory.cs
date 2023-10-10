using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.Examinations
{
    public interface IExamTitleReadOnlyFactory
    {
        Task<IExamTitleReadOnly> GetByExamIdAsync(System.Collections.Generic.List`1[System.String]);
    }
}
