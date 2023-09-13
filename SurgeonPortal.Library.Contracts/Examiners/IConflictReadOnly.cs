using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Examiners
{
    public interface IConflictReadOnly : IYtgReadOnlyBase
    {
        Guid StreamId { get; }
        string name { get; }
    }
}
