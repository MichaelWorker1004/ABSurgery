using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class LanguageReadOnlyListFactory : ILanguageReadOnlyListFactory
    {
        public async Task<ILanguageReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<LanguageReadOnlyList>();
            
        }

            


    }
}
