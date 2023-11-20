using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.MedicalTraining
{
    public class MedicalTrainingDal : SqlServerDalBase, IMedicalTrainingDal
    {
        public MedicalTrainingDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<MedicalTrainingDto> GetByUserIdAsync(int userId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<MedicalTrainingDto>(
                    "[dbo].[get_medical_training_byuserid]",
                        new
                        {
                            UserId = userId,
                        });
                        
            }
        }

        public async Task<MedicalTrainingDto> InsertAsync(MedicalTrainingDto dto)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    return await connection.ExecFirstOrDefaultAsync<MedicalTrainingDto>(
                        "[dbo].[ins_medical_training]",
                            new
                            {
                                UserId = dto.UserId,
                                GraduateProfileId = dto.GraduateProfileId,
                                MedicalSchoolName = dto.MedicalSchoolName,
                                MedicalSchoolCity = dto.MedicalSchoolCity,
                                MedicalSchoolStateId = dto.MedicalSchoolStateId,
                                MedicalSchoolCountryId = dto.MedicalSchoolCountryId,
                                DegreeId = dto.DegreeId,
                                MedicalSchoolCompletionYear = dto.MedicalSchoolCompletionYear,
                                ResidencyProgramName = dto.ResidencyProgramName,
                                ResidencyCompletionYear = dto.ResidencyCompletionYear,
                                ResidencyProgramOther = dto.ResidencyProgramOther,
                                CreatedByUserId = dto.CreatedByUserId,
                            });
                            
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if(ex.Message.Contains("Cannot insert duplicate key"))
                {
                    throw new Ytg.Framework.Exceptions.ObjectExistsException("MedicalTraining");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<MedicalTrainingDto> UpdateAsync(MedicalTrainingDto dto)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<MedicalTrainingDto>(
                    "[dbo].[update_medical_training]",
                        new
                        {
                            Id = dto.Id,
                            UserId = dto.UserId,
                            GraduateProfileId = dto.GraduateProfileId,
                            MedicalSchoolName = dto.MedicalSchoolName,
                            MedicalSchoolCity = dto.MedicalSchoolCity,
                            MedicalSchoolStateId = dto.MedicalSchoolStateId,
                            MedicalSchoolCountryId = dto.MedicalSchoolCountryId,
                            DegreeId = dto.DegreeId,
                            MedicalSchoolCompletionYear = dto.MedicalSchoolCompletionYear,
                            ResidencyProgramName = dto.ResidencyProgramName,
                            ResidencyCompletionYear = dto.ResidencyCompletionYear,
                            ResidencyProgramOther = dto.ResidencyProgramOther,
                            LastUpdatedByUserId = dto.UserId,
                        });
                        
            }
        }

    }
}
