using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class ExamTypeReadOnlyListFactory : IExamTypeReadOnlyListFactory
    {
        public async Task<IExamTypeReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<ExamTypeReadOnlyList>();
            
        }

            


    }
}
