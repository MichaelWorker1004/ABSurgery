using Microsoft.Extensions.Logging;
using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.MedicalTraining
{
    public class UserCertificateDal : SqlServerDalBase, IUserCertificateDal
    {
        private readonly ILogger<UserCertificateDal> _logger;

        public UserCertificateDal(ISqlConnectionManager sqlConnectionManager,
                                  ILogger<UserCertificateDal> logger)
            : base(sqlConnectionManager)
        {
            _logger = logger;
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
                        "[dbo].[upsert_usercertificates]",
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
                    _logger.LogError($"UserCertificateDal InsertAsync FAILED when trying to call InsertAsync. 'Cannot insert duplicate key' Error Message = {ex.Message}");
                    throw new Ytg.Framework.Exceptions.ObjectExistsException("UserCertificate");
                }
                else
                {
                    _logger.LogError($"UserCertificateDal InsertAsync FAILED when trying to call InsertAsync. Error Message = {ex.Message}");
                    throw;
                }
            }
        }

    }
}
