using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.GraduateMedicalEducation
{
    public interface IOverlapConflictCommandDal
    {
        OverlapConflictCommandDto CheckOverlapConflicts(int userId, System.DateTime startDate, System.DateTime endDate, int? rotationId);
    }
}
