using SurgeonPortal.DataAccess.Contracts.MedicalTraining;
using SurgeonPortal.Shared;
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
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<AdvancedTrainingDto>(
                    "[dbo].[ins_advanced_training]",
                        new
                        {
                            UserId = dto.UserId,
                            TrainingTypeId = dto.TrainingTypeId,
                            ProgramId = dto.ProgramId,
                            Other = dto.Other,
                            StartDate = dto.StartDate,
                            EndDate = dto.EndDate,
                            CreatedByUserId = IdentityHelper.UserId
                        });
                        
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
                            UserId = dto.UserId,
                            TrainingTypeId = dto.TrainingTypeId,
                            ProgramId = dto.ProgramId,
                            Other = dto.Other,
                            StartDate = dto.StartDate,
                            EndDate = dto.EndDate,
                            LastUpdatedByUserId = IdentityHelper.UserId
                        });
                        
            }
        }

    }
}
