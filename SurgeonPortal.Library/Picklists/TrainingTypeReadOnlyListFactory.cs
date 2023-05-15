using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class TrainingTypeReadOnlyListFactory : ITrainingTypeReadOnlyListFactory
    {
        public async Task<ITrainingTypeReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<TrainingTypeReadOnlyList>();
            
        }

            


    }
}
