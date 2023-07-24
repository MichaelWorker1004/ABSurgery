using Csla;
using SurgeonPortal.Library.Contracts.Picklists;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Picklists
{
    public class ExamStatusReadOnlyListFactory : IExamStatusReadOnlyListFactory
    {
        public async Task<IExamStatusReadOnlyList> GetAllAsync()
        {
            
            return await DataPortal.FetchAsync<ExamStatusReadOnlyList>();
            
        }

            


    }
}
