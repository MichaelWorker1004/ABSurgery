using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class FellowshipTypeReadOnlyFactory : IFellowshipTypeReadOnlyFactory
    {
        public async Task<IFellowshipTypeReadOnly> GetAsync()
        {
            
            return await DataPortal.FetchAsync<FellowshipTypeReadOnly>();
            
        }


        
            


    }
}
