using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.MedicalTraining
{
    public interface IAdvancedTraining : IYtgBusinessBase
    {
        int Id { get; set; }
        int UserId { get; set; }
        int TrainingTypeId { get; set; }
        string TrainingType { get;  }
        int? ProgramId { get; set; }
        string InstitutionName { get;  }
        string City { get;  }
        string State { get;  }
        string Other { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
    }
}
