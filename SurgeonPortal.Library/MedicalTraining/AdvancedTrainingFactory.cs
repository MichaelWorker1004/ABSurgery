using Csla;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.MedicalTraining
{
    public class AdvancedTrainingFactory : IAdvancedTrainingFactory
    {
        public async Task<IAdvancedTraining> GetByTrainingIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new FactoryInvalidCriteriaException("id is a required field.");
            }
            
            return await DataPortal.FetchAsync<AdvancedTraining>(
                new GetByTrainingIdCriteria(id));
            
        }

        public IAdvancedTraining Create()
        {
            return DataPortal.Create<AdvancedTraining>();
        }


        
            [Serializable]
            internal class GetByTrainingIdCriteria
            {
                public int Id { get; set; }
            
                public GetByTrainingIdCriteria(int id)
             {
                    Id = id;
              }
            }
            


    }
}
