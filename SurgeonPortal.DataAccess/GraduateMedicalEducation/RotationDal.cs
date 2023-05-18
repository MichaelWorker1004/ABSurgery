using SurgeonPortal.DataAccess.Contracts.GraduateMedicalEducation;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.GraduateMedicalEducation
{
    public class RotationDal : SqlServerDalBase, IRotationDal
    {
        public RotationDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task DeleteAsync(RotationDto dto)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecAsync(
                    "[dbo].[delete_gmerotations_byid]",
                        new
                        {
                            Id = dto.Id,
                        });
                        
            }
        }

        public async Task<RotationDto> GetByIdAsync(int id)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<RotationDto>(
                    "[dbo].[get_gmerotations_byid]",
                        new
                        {
                            Id = id,
                        });
                        
            }
        }

        public async Task<RotationDto> InsertAsync(RotationDto dto)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<RotationDto>(
                    "[dbo].[ins_gmerotations]",
                        new
                        {
                            UserId = dto.UserId,
                            StartDate = dto.StartDate,
                            EndDate = dto.EndDate,
                            ClinicalLevelId = dto.ClinicalLevelId,
                            ClinicalActivityId = dto.ClinicalActivityId,
                            ProgramName = dto.ProgramName,
                            NonSurgicalActivity = dto.NonSurgicalActivity,
                            AlternateInstitutionName = dto.AlternateInstitutionName,
                            IsInternationalRotation = dto.IsInternationalRotation,
                            Other = dto.Other,
                            CreatedByUserId = dto.CreatedByUserId,
                        });
                        
            }
        }

        public async Task<RotationDto> UpdateAsync(RotationDto dto)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<RotationDto>(
                    "[dbo].[update_gmerotations]",
                        new
                        {
                            Id = dto.Id,
                            UserId = dto.UserId,
                            StartDate = dto.StartDate,
                            EndDate = dto.EndDate,
                            ClinicalLevelId = dto.ClinicalLevelId,
                            ClinicalActivityId = dto.ClinicalActivityId,
                            ProgramName = dto.ProgramName,
                            NonSurgicalActivity = dto.NonSurgicalActivity,
                            AlternateInstitutionName = dto.AlternateInstitutionName,
                            IsInternationalRotation = dto.IsInternationalRotation,
                            Other = dto.Other,
                            LastUpdatedByUserId = dto.LastUpdatedByUserId,
                        });
                        
            }
        }

    }
}