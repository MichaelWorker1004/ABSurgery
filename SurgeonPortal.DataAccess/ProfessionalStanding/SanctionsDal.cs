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



        public async Task<SanctionsDto> GetByUserIdAsync(int userId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<SanctionsDto>(
                    "[dbo].[get_user_sanctions_byuserid]",
                        new
                        {
                            UserId = userId,
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
                                UserId = dto.UserId,
                                HadDrugAlchoholTreatment = dto.HadDrugAlchoholTreatment,
                                HadHospitalPrivilegesDenied = dto.HadHospitalPrivilegesDenied,
                                HadLicenseRestricted = dto.HadLicenseRestricted,
                                HadHospitalPrivilegesRestricted = dto.HadHospitalPrivilegesRestricted,
                                HadFelonyConviction = dto.HadFelonyConviction,
                                HadCensure = dto.HadCensure,
                                Explanation = dto.Explanation,
                                CreatedByUserId = dto.CreatedByUserId,
                                LastUpdatedByUserId = dto.LastUpdatedByUserId,
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
                            UserId = dto.UserId,
                            HadDrugAlchoholTreatment = dto.HadDrugAlchoholTreatment,
                            HadHospitalPrivilegesDenied = dto.HadHospitalPrivilegesDenied,
                            HadLicenseRestricted = dto.HadLicenseRestricted,
                            HadHospitalPrivilegesRestricted = dto.HadHospitalPrivilegesRestricted,
                            HadFelonyConviction = dto.HadFelonyConviction,
                            HadCensure = dto.HadCensure,
                            Explanation = dto.Explanation,
                            LastUpdatedByUserId = dto.UserId,
                        });
                        
            }
        }

    }
}
