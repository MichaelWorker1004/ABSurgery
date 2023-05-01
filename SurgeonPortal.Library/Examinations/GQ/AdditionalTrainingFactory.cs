using Csla;
using SurgeonPortal.Library.Contracts.Examinations.GQ;
using System;
using System.Threading.Tasks;

namespace SurgeonPortal.Library.Examinations.GQ
{
    public class AdditionalTrainingFactory : IAdditionalTrainingFactory
    {
        public async Task<IAdditionalTraining> GetByTrainingIdAsync(int trainingId)
        {
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
