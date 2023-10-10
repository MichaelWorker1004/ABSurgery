using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.MedicalTraining
{
    public class AdvancedTrainingDal : SqlServerDalBase, IAdvancedTrainingDal
    {
        public AdvancedTrainingDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<AdvancedTrainingDto> GetByTrainingIdAsync(int id)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<AdvancedTrainingDto>(
                    "[dbo].[get_advanced_training_by_trainingid]",
                        new
                        {
                            Id = id,
                        });
                        
            }
        }

        public async Task<AdvancedTrainingDto> InsertAsync(AdvancedTrainingDto dto)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    return await connection.ExecFirstOrDefaultAsync<AdvancedTrainingDto>(
                        "[dbo].[ins_advanced_training]",
                            new
                            {
                                UserId = dto.,
                                TrainingTypeId = dto.TrainingTypeId,
                                ProgramId = dto.ProgramId,
                                Other = dto.Other,
                                StartDate = dto.StartDate,
                                EndDate = dto.EndDate,
                                CreatedByUserId = dto.,
                            });
                            
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if(ex.Message.Contains("Cannot insert duplicate key"))
                {
                    throw new Ytg.Framework.Exceptions.ObjectExistsException("AdvancedTraining");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<AdvancedTrainingDto> UpdateAsync(AdvancedTrainingDto dto)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<AdvancedTrainingDto>(
                    "[dbo].[update_advanced_training]",
                        new
                        {
                            Id = dto.Id,
                            UserId = dto.,
                            TrainingTypeId = dto.TrainingTypeId,
                            ProgramId = dto.ProgramId,
                            Other = dto.Other,
                            StartDate = dto.StartDate,
                            EndDate = dto.EndDate,
                            LastUpdatedByUserId = dto.,
                        });
                        
            }
        }

    }
}
