using Csla;
using SurgeonPortal.Library.Contracts.Examinations.GQ;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Examinations.GQ
{
    public class AdditionalTrainingReadOnlyListFactory : IAdditionalTrainingReadOnlyListFactory
    {
        public async Task<IAdditionalTrainingReadOnlyList> GetAllByUserIdAsync()
        {
            
            return await DataPortal.FetchAsync<AdditionalTrainingReadOnlyList>();
            
        }

            


    }
}
