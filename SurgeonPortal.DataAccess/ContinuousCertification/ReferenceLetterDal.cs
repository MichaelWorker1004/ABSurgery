using SurgeonPortal.DataAccess.Contracts.ContinuousCertification;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.ContinuousCertification
{
    public class ReferenceLetterDal : SqlServerDalBase, IReferenceLetterDal
    {
        public ReferenceLetterDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<ReferenceLetterDto> GetByIdAsync(int id)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<ReferenceLetterDto>(
                    "[dbo].[get_refLet_details_byId]",
                        new
                        {
                            Id = id,
                        });
                        
            }
        }

        public async Task<ReferenceLetterDto> InsertAsync(ReferenceLetterDto dto)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    return await connection.ExecFirstOrDefaultAsync<ReferenceLetterDto>(
                        "[dbo].[ins_cc_refLetter]",
                            new
                            {
                                UserId = dto.UserId,
                                Official = dto.Official,
                                Title = dto.Title,
                                RoleId = dto.RoleId,
                                AltRoleId = dto.AltRoleId,
                                Explain = dto.Explain,
                                Email = dto.Email,
                                Phone = dto.Phone,
                                Hosp = dto.Hosp,
                                City = dto.City,
                                State = dto.State,
                            });
                            
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if(ex.Message.Contains("Cannot insert duplicate key"))
                {
                    throw new Ytg.Framework.Exceptions.ObjectExistsException("ReferenceLetter");
                }
                else
                {
                    throw;
                }
            }
        }

    }
}
