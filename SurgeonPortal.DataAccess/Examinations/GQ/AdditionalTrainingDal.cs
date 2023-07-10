using SurgeonPortal.DataAccess.Contracts.Examinations.GQ;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Examinations.GQ
{
    public class AdditionalTrainingDal : SqlServerDalBase, IAdditionalTrainingDal
    {
        public AdditionalTrainingDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<AdditionalTrainingDto> GetByTrainingIdAsync(int trainingId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<AdditionalTrainingDto>(
                    "[dbo].[get_additionaltraining_bytrainingid]",
                        new
                        {
                            TrainingId = trainingId,
                        });
                        
            }
        }

        public async Task<AdditionalTrainingDto> InsertAsync(AdditionalTrainingDto dto)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    return await connection.ExecFirstOrDefaultAsync<AdditionalTrainingDto>(
                        "[dbo].[ins_additionaltraining_bytrainingid]",
                            new
                            {
                                DateEnded = dto.DateEnded,
                                DateStarted = dto.DateStarted,
                                Other = dto.Other,
                                InstitutionId = dto.InstitutionId,
                                City = dto.City,
                                StateId = dto.StateId,
                                TypeOfTraining = dto.TypeOfTraining,
                            });
                            
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if(ex.Message.Contains("Cannot insert duplicate key"))
                {
                    throw new Ytg.Framework.Exceptions.ObjectExistsException("AdditionalTraining");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<AdditionalTrainingDto> UpdateAsync(AdditionalTrainingDto dto)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<AdditionalTrainingDto>(
                    "[dbo].[update_additionaltraining_bytrainingid]",
                        new
                        {
                            TrainingId = dto.TrainingId,
                            DateEnded = dto.DateEnded,
                            DateStarted = dto.DateStarted,
                            Other = dto.Other,
                            InstitutionId = dto.InstitutionId,
                            City = dto.City,
                            StateId = dto.StateId,
                            TypeOfTraining = dto.TypeOfTraining,
                        });
                        
            }
        }

    }
}
