using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.MedicalTraining
{
    public class UserCertificateDal : SqlServerDalBase, IUserCertificateDal
    {
        public UserCertificateDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task DeleteAsync(UserCertificateDto dto)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecAsync(
                    "[dbo].[del_usercertificate]",
                        new
                        {
                            CertificateId = dto.CertificateId,
                            UserId = dto.UserId,
                        });
                        
            }
        }

        public async Task<UserCertificateDto> GetByIdAsync(int certificateId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<UserCertificateDto>(
                    "[dbo].[get_usercertificates_byid]",
                        new
                        {
                            CertificateId = certificateId,
                        });
                        
            }
        }

        public async Task<UserCertificateDto> InsertAsync(UserCertificateDto dto)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    return await connection.ExecFirstOrDefaultAsync<UserCertificateDto>(
                        "[dbo].[insert_usercertificates]",
                            new
                            {
                                UserId = dto.UserId,
                                DocumentId = dto.DocumentId,
                                CertificateTypeId = dto.CertificateTypeId,
                                IssueDate = dto.IssueDate,
                                CertificateNumber = dto.CertificateNumber,
                                CreatedByUserId = dto.UserId,
                            });
                            
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if(ex.Message.Contains("Cannot insert duplicate key"))
                {
                    throw new Ytg.Framework.Exceptions.ObjectExistsException("UserCertificate");
                }
                else
                {
                    throw;
                }
            }
        }

    }
}
