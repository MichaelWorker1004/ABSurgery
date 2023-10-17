using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.GraduateMedicalEducation
{
    public interface IOverlapConflictCommandFactory
    {
        IOverlapConflictCommand CheckOverlapConflicts(
            int userId,
            System.DateTime startDate,
            System.DateTime endDate,
            int? rotationId);
    }
}
