using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class JcahoOrganizationReadOnlyListFactory : IJcahoOrganizationReadOnlyListFactory
    {
        public async Task<IJcahoOrganizationReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<JcahoOrganizationReadOnlyList>();
            
        }

            


    }
}
