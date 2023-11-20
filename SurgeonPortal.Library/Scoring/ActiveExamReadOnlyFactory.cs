using Csla;
using SurgeonPortal.Library.Contracts.Scoring;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Scoring
{
    public class ActiveExamReadOnlyFactory : IActiveExamReadOnlyFactory
    {
        public async Task<IActiveExamReadOnly> GetByExaminerUserIdAsync(DateTime currentDate)
        {
            
            return await DataPortal.FetchAsync<ActiveExamReadOnly>(
                new GetByExaminerUserIdCriteria(currentDate));
            
        }


        
            [Serializable]
            internal class GetByExaminerUserIdCriteria
            {
                public DateTime CurrentDate { get; set; }
            
                public GetByExaminerUserIdCriteria(DateTime currentDate)
             {
                    CurrentDate = currentDate;
              }
            }
            


    }
}
