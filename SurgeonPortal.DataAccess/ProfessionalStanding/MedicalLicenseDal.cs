using SurgeonPortal.DataAccess.Contracts.ProfessionalStanding;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.ProfessionalStanding
{
    public class MedicalLicenseDal : SqlServerDalBase, IMedicalLicenseDal
    {
        public MedicalLicenseDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task DeleteAsync(MedicalLicenseDto dto)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecAsync(
                    "[dbo].[del_userlicense]",
                        new
                        {
                            LicenseId = dto.LicenseId,
                            UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                        });
                        
            }
        }

        public async Task<MedicalLicenseDto> GetByIdAsync(int licenseId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<MedicalLicenseDto>(
                    "[dbo].[get_userlicenses_byid]",
                        new
                        {
                            LicenseId = licenseId,
                        });
                        
            }
        }

        public async Task<MedicalLicenseDto> InsertAsync(MedicalLicenseDto dto)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    return await connection.ExecFirstOrDefaultAsync<MedicalLicenseDto>(
                        "[dbo].[insert_userlicenses]",
                            new
                            {
                                UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                                IssuingStateId = dto.IssuingStateId,
                                LicenseNumber = dto.LicenseNumber,
                                LicenseTypeId = dto.LicenseTypeId,
                                ReportingOrganization = dto.ReportingOrganization,
                                IssueDate = dto.IssueDate,
                                ExpireDate = dto.ExpireDate,
                            });
                            
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if(ex.Message.Contains("Cannot insert duplicate key"))
                {
                    throw new Ytg.Framework.Exceptions.ObjectExistsException("MedicalLicense");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<MedicalLicenseDto> UpdateAsync(MedicalLicenseDto dto)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<MedicalLicenseDto>(
                    "[dbo].[update_userlicenses]",
                        new
                        {
                            LicenseId = dto.LicenseId,
                            UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                            IssuingStateId = dto.IssuingStateId,
                            LicenseNumber = dto.LicenseNumber,
                            LicenseTypeId = dto.LicenseTypeId,
                            ReportingOrganization = dto.ReportingOrganization,
                            IssueDate = dto.IssueDate,
                            ExpireDate = dto.ExpireDate,
                        });
                        
            }
        }

    }
}
