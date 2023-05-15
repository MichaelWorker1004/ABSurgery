using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class AccreditedProgramInstitutionReadOnlyListFactory : IAccreditedProgramInstitutionReadOnlyListFactory
    {
        public async Task<IAccreditedProgramInstitutionReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<AccreditedProgramInstitutionReadOnlyList>();
            
        }

            


    }
}
