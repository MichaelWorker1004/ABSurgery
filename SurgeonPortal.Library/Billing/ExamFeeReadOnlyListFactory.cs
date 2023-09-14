using Csla;
using SurgeonPortal.Library.Contracts.Billing;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Billing
{
    public class ExamFeeReadOnlyListFactory : IExamFeeReadOnlyListFactory
    {
        public async Task<IExamFeeReadOnlyList> GetByUserIdAsync()
        {
            
            return await DataPortal.FetchAsync<ExamFeeReadOnlyList>();
            
        }

            


    }
}
