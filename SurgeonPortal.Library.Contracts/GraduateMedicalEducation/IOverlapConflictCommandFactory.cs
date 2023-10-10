using System.Threading.Tasks;

namespace SurgeonPortal.Library.Contracts.GraduateMedicalEducation
{
    public interface IOverlapConflictCommandFactory
    {
        IOverlapConflictCommand CheckOverlapConflicts(System.Collections.Generic.List`1[System.String]);
    }
}
