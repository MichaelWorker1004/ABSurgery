using Csla;
using SurgeonPortal.Library.Contracts.Examinations;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Examinations
{
    public class ExamHistoryReadOnlyListFactory : IExamHistoryReadOnlyListFactory
    {
        public async Task<IExamHistoryReadOnlyList> GetByUserIdAsync()
        {
            
            return await DataPortal.FetchAsync<ExamHistoryReadOnlyList>();
            
        }

            [Serializable]
            internal class GetByUserIdCriteria
            {
            
                public GetByUserIdCriteria()
             {
              }
            }
            


    }
}
