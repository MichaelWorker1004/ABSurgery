using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using SurgeonPortal.Shared;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.MedicalTraining
{
    public class FellowshipDal : SqlServerDalBase, IFellowshipDal
    {
        public FellowshipDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task DeleteAsync(FellowshipDto dto)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecAsync(
                    "[dbo].[delete_userfellowships]",
                        new
                        {
                            Id = dto.Id,
                        });
                        
            }
        }

        public async Task<FellowshipDto> GetByIdAsync(int id)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<FellowshipDto>(
                    "[dbo].[get_userfellowships_byid]",
                        new
                        {
                            Id = id,
                        });
                        
            }
        }

        public async Task<FellowshipDto> InsertAsync(FellowshipDto dto)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<FellowshipDto>(
                    "[dbo].[ins_userfellowships]",
                        new
                        {
                            UserId = dto.UserId,
                            ProgramName = dto.ProgramName,
                            CompletionYear = dto.CompletionYear,
                            ProgramOther = dto.ProgramOther,
                            CreatedByUserId = IdentityHelper.UserId,
                        });
                        
            }
        }

        public async Task<FellowshipDto> UpdateAsync(FellowshipDto dto)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<FellowshipDto>(
                    "[dbo].[update_userfellowships]",
                        new
                        {
                            Id = dto.Id,
                            UserId = dto.UserId,
                            ProgramName = dto.ProgramName,
                            CompletionYear = dto.CompletionYear,
                            ProgramOther = dto.ProgramOther,
                            LastUpdatedByUserId = IdentityHelper.UserId,
                        });
                        
            }
        }

    }
}
