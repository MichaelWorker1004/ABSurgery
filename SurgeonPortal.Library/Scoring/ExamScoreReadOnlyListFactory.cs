using Csla;
using SurgeonPortal.Library.Contracts.Scoring;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Exceptions;

namespace SurgeonPortal.Library.Scoring
{
    public class ExamScoreReadOnlyListFactory : IExamScoreReadOnlyListFactory
    {
        public async Task<IExamScoreReadOnlyList> GetByIdAsync()
        {
            
            return await DataPortal.FetchAsync<ExamScoreReadOnlyList>();
            
        }

            


    }
}
