using Csla;
using SurgeonPortal.Library.Contracts.Scoring.CE;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Scoring.CE
{
    public class ExamineeReadOnlyFactory : IExamineeReadOnlyFactory
    {
        public async Task<IExamineeReadOnly> GetByIdAsync(int examScheduleId)
        {
            if (examScheduleId <= 0)
            {
                throw new FactoryInvalidCriteriaException("examScheduleId is a required field.");
            }
            
            return await DataPortal.FetchAsync<ExamineeReadOnly>(
                new GetByIdCriteria(examScheduleId));
            
        }


        
            [Serializable]
            internal class GetByIdCriteria
            {
                public int ExamScheduleId { get; set; }
            
                public GetByIdCriteria(int examScheduleId)
             {
                    ExamScheduleId = examScheduleId;
              }
            }
            


    }
}
