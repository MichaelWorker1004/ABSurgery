using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class ExamSpecialtyReadOnlyListFactory : IExamSpecialtyReadOnlyListFactory
    {
        public async Task<IExamSpecialtyReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<ExamSpecialtyReadOnlyList>();
            
        }

            


    }
}
