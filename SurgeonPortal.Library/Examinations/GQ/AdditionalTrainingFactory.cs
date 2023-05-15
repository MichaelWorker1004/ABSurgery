using Csla;
using SurgeonPortal.Library.Contracts.Examinations.GQ;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Examinations.GQ
{
    public class AdditionalTrainingFactory : IAdditionalTrainingFactory
    {
        public async Task<IAdditionalTraining> GetByTrainingIdAsync(int trainingId)
        {
            if (trainingId <= 0)
            {
                throw new FactoryInvalidCriteriaException("trainingId is a required field.");
            }
            
            return await DataPortal.FetchAsync<AdditionalTraining>(
                new GetByTrainingIdCriteria(trainingId));
            
        }

        public IAdditionalTraining Create()
        {
            return DataPortal.Create<AdditionalTraining>();
        }


        
            [Serializable]
            internal class GetByTrainingIdCriteria
            {
                public int TrainingId { get; set; }
            
                public GetByTrainingIdCriteria(int trainingId)
             {
                    TrainingId = trainingId;
              }
            }
            


    }
}
