using SurgeonPortal.DataAccess.Contracts.GraduateMedicalEducation;
using System;
using System.Threading.Tasks;
using Ytg.Framework.ConnectionManager;
using Ytg.Framework.SqlServer;

namespace SurgeonPortal.DataAccess.GraduateMedicalEducation
{
    public class OverlapConflictCommandDal : SqlServerDalBase, IOverlapConflictCommandDal
    {
        public OverlapConflictCommandDal(ISqlConnectionManager sqlConnectionManager)
            : base(sqlConnectionManager)
        {
        }



        public OverlapConflictCommandDto CheckOverlapConflicts(
            int userId,
            DateTime startDate,
            DateTime endDate)
        {
            using (var connection = CreateConnection())
            {
                return connection.ExecFirstOrDefault<OverlapConflictCommandDto>(
                    "[dbo].[get_gme_overlap_conflict]",
                        new
                        {
                            UserId = userId,
                            StartDate = startDate,
                            EndDate = endDate,
                        });
                        
            }
        }

    }
}
