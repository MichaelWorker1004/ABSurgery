using SurgeonPortal.DataAccess.Contracts.ProfessionalStanding;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.ProfessionalStanding
{
    public class SanctionsDal : SqlServerDalBase, ISanctionsDal
    {
        public SanctionsDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<SanctionsDto> GetByUserIdAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<SanctionsDto>(
                    "[dbo].[get_user_sanctions_byuserid]",
                        new
                        {
                            UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                        });
                        
            }
        }

        public async Task<SanctionsDto> InsertAsync(SanctionsDto dto)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    return await connection.ExecFirstOrDefaultAsync<SanctionsDto>(
                        "[dbo].[ins_user_sanctions]",
                            new
                            {
                                UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                                HadDrugAlchoholTreatment = dto.HadDrugAlchoholTreatment,
                                HadHospitalPrivilegesDenied = dto.HadHospitalPrivilegesDenied,
                                HadLicenseRestricted = dto.HadLicenseRestricted,
                                HadHospitalPrivilegesRestricted = dto.HadHospitalPrivilegesRestricted,
                                HadFelonyConviction = dto.HadFelonyConviction,
                                HadCensure = dto.HadCensure,
                                Explanation = dto.Explanation,
                                CreatedByUserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                                LastUpdatedByUserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                            });
                            
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if(ex.Message.Contains("Cannot insert duplicate key"))
                {
                    throw new Ytg.Framework.Exceptions.ObjectExistsException("Sanctions");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<SanctionsDto> UpdateAsync(SanctionsDto dto)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<SanctionsDto>(
                    "[dbo].[update_user_sanctions_byuserid]",
                        new
                        {
                            UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                            HadDrugAlchoholTreatment = dto.HadDrugAlchoholTreatment,
                            HadHospitalPrivilegesDenied = dto.HadHospitalPrivilegesDenied,
                            HadLicenseRestricted = dto.HadLicenseRestricted,
                            HadHospitalPrivilegesRestricted = dto.HadHospitalPrivilegesRestricted,
                            HadFelonyConviction = dto.HadFelonyConviction,
                            HadCensure = dto.HadCensure,
                            Explanation = dto.Explanation,
                            LastUpdatedByUserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                        });
                        
            }
        }

    }
}
