using SurgeonPortal.DataAccess.Contracts.Examinations;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Examinations
{
    public class AccommodationDal : SqlServerDalBase, IAccommodationDal
    {
        public AccommodationDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task DeleteAsync(AccommodationDto dto)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecAsync(
                    "[dbo].[delete_user_accommodations]",
                        new
                        {
                            Id = dto.Id,
                        });
                        
            }
        }

        public async Task<AccommodationDto> GetByExamIdAsync(
            int userId,
            int examId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<AccommodationDto>(
                    "[dbo].[get_user_accommodations_byId]",
                        new
                        {
                            UserId = userId,
                            ExamId = examId,
                        });
                        
            }
        }

        public async Task<AccommodationDto> InsertAsync(AccommodationDto dto)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    return await connection.ExecFirstOrDefaultAsync<AccommodationDto>(
                        "[dbo].[insert_user_accommodations]",
                            new
                            {
                                UserId = dto.UserId,
                                AccommodationID = dto.AccommodationID,
                                DocumentId = dto.DocumentId,
                                ExamID = dto.ExamID,
                                CreatedByUserId = dto.CreatedByUserId,
                                LastUpdatedByUserId = dto.LastUpdatedByUserId,
                            });
                            
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if(ex.Message.Contains("Cannot insert duplicate key"))
                {
                    throw new Ytg.Framework.Exceptions.ObjectExistsException("Accommodation");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<AccommodationDto> UpdateAsync(AccommodationDto dto)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<AccommodationDto>(
                    "[dbo].[update_user_accommodations]",
                        new
                        {
                            UserId = dto.UserId,
                            Id = dto.Id,
                            AccommodationID = dto.AccommodationID,
                            DocumentId = dto.DocumentId,
                            ExamID = dto.ExamID,
                            LastUpdatedByUserId = dto.LastUpdatedByUserId,
                        });
                        
            }
        }

    }
}
