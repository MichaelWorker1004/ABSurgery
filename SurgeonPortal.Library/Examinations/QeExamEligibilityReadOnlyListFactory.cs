using Csla;
using SurgeonPortal.Library.Contracts.Examinations;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Examinations
{
    public class QeExamEligibilityReadOnlyListFactory : IQeExamEligibilityReadOnlyListFactory
    {
        public async Task<IQeExamEligibilityReadOnlyList> GetByUserIdAsync()
        {
            
            return await DataPortal.FetchAsync<QeExamEligibilityReadOnlyList>();
            
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
