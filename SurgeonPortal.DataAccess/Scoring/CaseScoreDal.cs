using SurgeonPortal.DataAccess.Contracts.Scoring;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Scoring
{
    public class CaseScoreDal : SqlServerDalBase, ICaseScoreDal
    {
        public CaseScoreDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task DeleteAsync(CaseScoreDto dto)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecAsync(
                    "[dbo].[delete_examinerscore_byid]",
                        new
                        {
                            ExamScoringId = dto.ExamScoringId,
                        });
                        
            }
        }

        public async Task<CaseScoreDto> GetByIdAsync(
            int examScoringId,
            int examinerUserId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<CaseScoreDto>(
                    "[dbo].[get_examcasescore_byid]",
                        new
                        {
                            ExamScoringId = examScoringId,
                            ExaminerUserId = examinerUserId,
                        });
                        
            }
        }

        public async Task<CaseScoreDto> InsertAsync(CaseScoreDto dto)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    return await connection.ExecFirstOrDefaultAsync<CaseScoreDto>(
                        "[dbo].[upsert_examinerscore]",
                            new
                            {
                                ExaminerUserId = dto.ExaminerUserId,
                                ExamineeUserId = dto.ExamineeUserId,
                                ExamCaseId = dto.ExamCaseId,
                                Score = dto.Score,
                                Remarks = dto.Remarks,
                                CriticalFail = dto.CriticalFail,
                            });
                            
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if(ex.Message.Contains("Cannot insert duplicate key"))
                {
                    throw new Ytg.Framework.Exceptions.ObjectExistsException("CaseScore");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<CaseScoreDto> UpdateAsync(CaseScoreDto dto)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<CaseScoreDto>(
                    "[dbo].[upsert_examinerscore]",
                        new
                        {
                            ExaminerUserId = dto.ExaminerUserId,
                            ExamineeUserId = dto.ExamineeUserId,
                            ExamCaseId = dto.ExamCaseId,
                            Score = dto.Score,
                            Remarks = dto.Remarks,
                            CriticalFail = dto.CriticalFail,
                        });
                        
            }
        }

    }
}
