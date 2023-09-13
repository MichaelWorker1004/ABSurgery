using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.Examiners
{
    public interface IAgendaReadOnly : IYtgReadOnlyBase
    {
        Guid StreamId { get; }
        string Name { get; }
    }
}
