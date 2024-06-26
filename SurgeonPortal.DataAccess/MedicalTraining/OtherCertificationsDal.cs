using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.MedicalTraining
{
    public class OtherCertificationsDal : SqlServerDalBase, IOtherCertificationsDal
    {
        public OtherCertificationsDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task DeleteAsync(OtherCertificationsDto dto)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecAsync(
                    "[dbo].[del_user_certificates_other_byid]",
                        new
                        {
                            Id = dto.Id,
                        });
                        
            }
        }

        public async Task<OtherCertificationsDto> GetByIdAsync(int id)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<OtherCertificationsDto>(
                    "[dbo].[get_user_certificates_other_byid]",
                        new
                        {
                            Id = id,
                        });
                        
            }
        }

        public async Task<OtherCertificationsDto> InsertAsync(OtherCertificationsDto dto)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    return await connection.ExecFirstOrDefaultAsync<OtherCertificationsDto>(
                        "[dbo].[ins_user_certificates_other]",
                            new
                            {
                                UserId = dto.UserId,
                                CertificateTypeId = dto.CertificateTypeId,
                                IssueDate = dto.IssueDate,
                                CertificateNumber = dto.CertificateNumber,
                                CreatedByUserId = dto.CreatedByUserId,
                            });
                            
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if(ex.Message.Contains("Cannot insert duplicate key"))
                {
                    throw new Ytg.Framework.Exceptions.ObjectExistsException("OtherCertifications");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<OtherCertificationsDto> UpdateAsync(OtherCertificationsDto dto)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<OtherCertificationsDto>(
                    "[dbo].[update_user_certificates_other_byid]",
                        new
                        {
                            Id = dto.Id,
                            CertificateTypeId = dto.CertificateTypeId,
                            IssueDate = dto.IssueDate,
                            CertificateNumber = dto.CertificateNumber,
                            LastUpdatedByUserId = dto.UserId,
                        });
                        
            }
        }

    }
}
