using SurgeonPortal.DataAccess.Contracts.ProfessionalStanding;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.ProfessionalStanding
{
    public class UserAppointmentDal : SqlServerDalBase, IUserAppointmentDal
    {
        public UserAppointmentDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task DeleteAsync(UserAppointmentDto dto)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecAsync(
                    "[dbo].[del_userhospappt]",
                        new
                        {
                            ApptId = dto.ApptId,
                            UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                        });
                        
            }
        }

        public async Task<UserAppointmentDto> GetByIdAsync(int apptId)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<UserAppointmentDto>(
                    "[dbo].[get_userhospappts_byid]",
                        new
                        {
                            ApptId = apptId,
                        });
                        
            }
        }

        public async Task<UserAppointmentDto> InsertAsync(UserAppointmentDto dto)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    return await connection.ExecFirstOrDefaultAsync<UserAppointmentDto>(
                        "[dbo].[insert_userhospappt]",
                            new
                            {
                                UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                                PracticeTypeId = dto.PracticeTypeId,
                                AppointmentTypeId = dto.AppointmentTypeId,
                                OrganizationTypeId = dto.OrganizationTypeId,
                                StateCode = dto.StateCode,
                                OrganizationId = dto.OrganizationId,
                                AuthorizingOfficial = dto.AuthorizingOfficial,
                                Other = dto.Other,
                                CreatedByUserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                            });
                            
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if(ex.Message.Contains("Cannot insert duplicate key"))
                {
                    throw new Ytg.Framework.Exceptions.ObjectExistsException("UserAppointment");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<UserAppointmentDto> UpdateAsync(UserAppointmentDto dto)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<UserAppointmentDto>(
                    "[dbo].[update_userhospappt]",
                        new
                        {
                            ApptId = dto.ApptId,
                            UserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                            PracticeTypeId = dto.PracticeTypeId,
                            AppointmentTypeId = dto.AppointmentTypeId,
                            OrganizationTypeId = dto.OrganizationTypeId,
                            StateCode = dto.StateCode,
                            OrganizationId = dto.OrganizationId,
                            AuthorizingOfficial = dto.AuthorizingOfficial,
                            Other = dto.Other,
                            LastUpdatedByUserId = SurgeonPortal.Shared.IdentityHelper.UserId,
                        });
                        
            }
        }

    }
}
