using Csla;
using SurgeonPortal.Library.Contracts.Examinations;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Examinations
{
    public class QualifyingExamReadOnlyFactory : IQualifyingExamReadOnlyFactory
    {
        public async Task<IQualifyingExamReadOnly> GetAsync()
        {
            
            return await DataPortal.FetchAsync<QualifyingExamReadOnly>();
            
        }


        
            


    }
}
