using Csla;
using SurgeonPortal.Library.Contracts.Examinations;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Examinations
{
    public class ExamOverviewReadOnlyListFactory : IExamOverviewReadOnlyListFactory
    {
        public async Task<IExamOverviewReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<ExamOverviewReadOnlyList>();
            
        }

            


    }
}
