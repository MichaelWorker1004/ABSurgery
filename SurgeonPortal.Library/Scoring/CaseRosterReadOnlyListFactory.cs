using Csla;
using SurgeonPortal.Library.Contracts.Scoring;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Scoring
{
    public class CaseRosterReadOnlyListFactory : ICaseRosterReadOnlyListFactory
    {
        public async Task<ICaseRosterReadOnlyList> GetByScheduleIdAsync(
            int scheduleId1,
            int? scheduleId2)
        {
            if (scheduleId1 <= 0)
            {
                throw new FactoryInvalidCriteriaException("scheduleId1 is a required field.");
            }
            
            return await DataPortal.FetchAsync<CaseRosterReadOnlyList>(
                new GetByScheduleIdCriteria(
                scheduleId1,
                scheduleId2));
            
        }

            [Serializable]
            internal class GetByScheduleIdCriteria
            {
                public int ScheduleId1 { get; set; }
                public int? ScheduleId2 { get; set; }
            
                public GetByScheduleIdCriteria(
                int scheduleId1,
                int? scheduleId2)
             {
                    ScheduleId1 = scheduleId1;
                    ScheduleId2 = scheduleId2;
              }
            }
            


    }
}
