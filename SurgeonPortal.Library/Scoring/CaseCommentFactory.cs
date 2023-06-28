using Csla;
using SurgeonPortal.Library.Contracts.Scoring;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Scoring
{
    public class CaseCommentFactory : ICaseCommentFactory
    {
        public async Task<ICaseComment> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new FactoryInvalidCriteriaException("id is a required field.");
            }
            
            return await DataPortal.FetchAsync<CaseComment>(
                new GetByIdCriteria(id));
            
        }

        public ICaseComment Create()
        {
            return DataPortal.Create<CaseComment>();
        }


        
            [Serializable]
            internal class GetByIdCriteria
            {
                public int Id { get; set; }
            
                public GetByIdCriteria(int id)
             {
                    Id = id;
              }
            }
            


    }
}
