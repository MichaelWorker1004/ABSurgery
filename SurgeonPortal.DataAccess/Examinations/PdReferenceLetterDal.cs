using SurgeonPortal.DataAccess.Contracts.Examinations;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Examinations
{
    public class PdReferenceLetterDal : SqlServerDalBase, IPdReferenceLetterDal
    {
        public PdReferenceLetterDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<PdReferenceLetterDto> GetByExamIdAsync(
            int userId,
            int examId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<PdReferenceLetterDto>(
                    "[dbo].[get_pd_refLetters]",
                        new
                        {
                            UserId = userId,
                            ExamId = examId,
                        });
                        
            }
        }

        public async Task<PdReferenceLetterDto> InsertAsync(PdReferenceLetterDto dto)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    return await connection.ExecFirstOrDefaultAsync<PdReferenceLetterDto>(
                        "[dbo].[ins_pd_refLetter]",
                            new
                            {
                                UserId = dto.UserId,
                                Official = dto.Official,
                                Title = dto.Title,
                                Email = dto.Email,
                                Hosp = dto.Hosp,
                                ExamId = dto.ExamId,
                                IdCode = dto.IdCode,
                            });
                            
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if(ex.Message.Contains("Cannot insert duplicate key"))
                {
                    throw new Ytg.Framework.Exceptions.ObjectExistsException("PdReferenceLetter");
                }
                else
                {
                    throw;
                }
            }
        }

    }
}
