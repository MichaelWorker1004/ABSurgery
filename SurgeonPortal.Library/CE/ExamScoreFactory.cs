using Csla;
using SurgeonPortal.Library.Contracts.CE;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.CE
{
    public class ExamScoreFactory : IExamScoreFactory
    {
        public async Task<IExamScore> GetByIdAsync(int examScheduleScoreId)
        {
            if (examScheduleScoreId <= 0)
            {
                throw new FactoryInvalidCriteriaException("examScheduleScoreId is a required field.");
            }
            
            return await DataPortal.FetchAsync<ExamScore>(
                new GetByIdCriteria(examScheduleScoreId));
            
        }

        public IExamScore Create()
        {
            return DataPortal.Create<ExamScore>();
        }


        
            [Serializable]
            internal class GetByIdCriteria
            {
                public int ExamScheduleScoreId { get; set; }
            
                public GetByIdCriteria(int examScheduleScoreId)
             {
                    ExamScheduleScoreId = examScheduleScoreId;
              }
            }
            


    }
}
