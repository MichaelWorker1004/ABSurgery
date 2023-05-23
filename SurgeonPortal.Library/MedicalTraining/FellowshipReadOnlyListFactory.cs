using Csla;
using SurgeonPortal.Library.Contracts.MedicalTraining;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.MedicalTraining
{
    public class FellowshipReadOnlyListFactory : IFellowshipReadOnlyListFactory
    {
        public async Task<IFellowshipReadOnlyList> GetByUserIdAsync()
        {
            
            return await DataPortal.FetchAsync<FellowshipReadOnlyList>();
            
        }

            


    }
}
