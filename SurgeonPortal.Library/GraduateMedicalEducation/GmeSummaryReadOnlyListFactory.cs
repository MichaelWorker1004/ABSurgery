using Csla;
using SurgeonPortal.Library.Contracts.GraduateMedicalEducation;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.GraduateMedicalEducation
{
    public class GmeSummaryReadOnlyListFactory : IGmeSummaryReadOnlyListFactory
    {
        public async Task<IGmeSummaryReadOnlyList> GetByUserIdAsync()
        {
            
            return await DataPortal.FetchAsync<GmeSummaryReadOnlyList>();
            
        }

            


    }
}
