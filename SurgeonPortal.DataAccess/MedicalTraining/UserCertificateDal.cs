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
                            UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                        });
                        
            }
        }

        public async Task<UserCertificateDto> GetByIdAsync(int id)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<UserCertificateDto>(
                    "[dbo].[get_usercertificates_byid]",
                        new
                        {
                            Id = id,
                        });
                        
            }
        }

        public async Task<UserCertificateDto> InsertAsync(UserCertificateDto dto)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<UserCertificateDto>(
                    "[dbo].[insert_usercertificates]",
                        new
                        {
                            UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                            DocumentId = dto.DocumentId,
                            CertificateTypeId = dto.CertificateTypeId,
                            IssueDate = dto.IssueDate,
                            CertificateNumber = dto.CertificateNumber,
                            CreatedByUserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                        });
                        
            }
        }

    }
}