using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class OrganizationTypeReadOnlyListFactory : IOrganizationTypeReadOnlyListFactory
    {
        public async Task<IOrganizationTypeReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<OrganizationTypeReadOnlyList>();
            
        }

            


    }
}
