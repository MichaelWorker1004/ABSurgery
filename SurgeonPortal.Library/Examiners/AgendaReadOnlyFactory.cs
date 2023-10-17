using Csla;
using SurgeonPortal.Library.Contracts.Examiners;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Examiners
{
    public class AgendaReadOnlyFactory : IAgendaReadOnlyFactory
    {
        public async Task<IAgendaReadOnly> GetByExamHeaderIdAsync(int examHeaderId)
        {
            if (examHeaderId <= 0)
            {
                throw new FactoryInvalidCriteriaException("examHeaderId is a required field.");
            }
            
            return await DataPortal.FetchAsync<AgendaReadOnly>(
                new GetByExamHeaderIdCriteria(examHeaderId));
            
        }


        
            [Serializable]
            internal class GetByExamHeaderIdCriteria
            {
                public int ExaminerUserId { get; set; }
                public int ExamHeaderId { get; set; }
            
                public GetByExamHeaderIdCriteria(
                int examinerUserId,
                int examHeaderId)
             {
                    ExaminerUserId = examinerUserId;
                    ExamHeaderId = examHeaderId;
              }
            }
            


    }
}
