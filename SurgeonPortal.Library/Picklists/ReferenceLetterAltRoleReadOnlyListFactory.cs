using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class ReferenceLetterAltRoleReadOnlyListFactory : IReferenceLetterAltRoleReadOnlyListFactory
    {
        public async Task<IReferenceLetterAltRoleReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<ReferenceLetterAltRoleReadOnlyList>();
            
        }

            


    }
}
