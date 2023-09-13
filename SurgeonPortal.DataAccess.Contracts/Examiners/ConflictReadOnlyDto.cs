using System;

namespace SurgeonPortal.DataAccess.Contracts.Examiners
{
    public class ConflictReadOnlyDto
    {
        public Guid StreamId { get; set; }
        public string Name { get; set; }
    }
}
