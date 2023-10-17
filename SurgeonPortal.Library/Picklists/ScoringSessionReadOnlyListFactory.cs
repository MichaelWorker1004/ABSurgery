using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class ScoringSessionReadOnlyListFactory : IScoringSessionReadOnlyListFactory
    {
        public async Task<IScoringSessionReadOnlyList> GetByKeysAsync(DateTime currentDate)
        {
            
            return await DataPortal.FetchAsync<ScoringSessionReadOnlyList>(
                new GetByKeysCriteria(currentDate));
            
        }

            [Serializable]
            internal class GetByKeysCriteria
            {
                public int ExaminerUserId { get; set; }
                public DateTime CurrentDate { get; set; }
            
                public GetByKeysCriteria(
                int examinerUserId,
                DateTime currentDate)
             {
                    ExaminerUserId = examinerUserId;
                    CurrentDate = currentDate;
              }
            }
            


    }
}
