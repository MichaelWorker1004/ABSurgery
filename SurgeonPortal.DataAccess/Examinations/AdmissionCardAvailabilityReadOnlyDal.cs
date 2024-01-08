using SurgeonPortal.DataAccess.Contracts.Examinations;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.Examinations
{
    public class AdmissionCardAvailabilityReadOnlyDal : SqlServerDalBase, IAdmissionCardAvailabilityReadOnlyDal
    {
        public AdmissionCardAvailabilityReadOnlyDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public async Task<AdmissionCardAvailabilityReadOnlyDto> GetByExamIdAsync(
            int userID,
            int examID)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecFirstOrDefaultAsync<AdmissionCardAvailabilityReadOnlyDto>(
                    "[dbo].[get_adm_card_availability]",
                        new
                        {
                            UserID = userID,
                            ExamID = examID,
                        });
                        
            }
        }

    }
}
