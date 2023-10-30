using Csla;
using SurgeonPortal.Library.Contracts.Scoring.CE;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Scoring.CE
{
    public class TitleReadOnlyListFactory : ITitleReadOnlyListFactory
    {
        public async Task<ITitleReadOnlyList> GetByIdAsync(
            int examScheduleId,
            int examineeUserId)
        {
            if (examScheduleId <= 0)
            {
                throw new FactoryInvalidCriteriaException("examScheduleId is a required field.");
            }
            if (examineeUserId <= 0)
            {
                throw new FactoryInvalidCriteriaException("examineeUserId is a required field.");
            }
            
            return await DataPortal.FetchAsync<TitleReadOnlyList>(
                new GetByIdCriteria(
                examScheduleId,
                examineeUserId));
            
        }

            [Serializable]
            internal class GetByIdCriteria
            {
                public int ExamScheduleId { get; set; }
                public int ExamineeUserId { get; set; }
            
                public GetByIdCriteria(
                int examScheduleId,
                int examineeUserId)
             {
                    ExamScheduleId = examScheduleId;
                    ExamineeUserId = examineeUserId;
              }
            }
            


    }
}
