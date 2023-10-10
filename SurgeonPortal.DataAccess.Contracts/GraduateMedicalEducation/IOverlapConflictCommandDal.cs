using System.Threading.Tasks;

namespace SurgeonPortal.DataAccess.Contracts.GraduateMedicalEducation
{
    public interface IOverlapConflictCommandDal
    {
        OverlapConflictCommandDto CheckOverlapConflicts(System.Collections.Generic.List`1[System.String]);
    }
}
