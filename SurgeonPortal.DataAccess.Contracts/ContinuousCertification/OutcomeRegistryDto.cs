using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.DataAccess.Contracts.ContinuousCertification
{
    public class OutcomeRegistryDto : YtgBusinessBaseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string SurgeonSpecificRegistry { get; set; }
        public string RegistryComments { get; set; }
        public bool RegisteredWithACHQC { get; set; }
        public bool RegisteredWithCESQIP { get; set; }
        public bool RegisteredWithMBSAQIP { get; set; }
        public bool RegisteredWithABA { get; set; }
        public bool RegisteredWithASBS { get; set; }
        public bool RegisteredWithMSQC { get; set; }
        public bool RegisteredWithABMS { get; set; }
        public bool RegisteredWithNCDB { get; set; }
        public bool RegisteredWithRQRS { get; set; }
        public bool RegisteredWithNSQIP { get; set; }
        public bool RegisteredWithNTDB { get; set; }
        public bool RegisteredWithSTS { get; set; }
        public bool RegisteredWithTQIP { get; set; }
        public bool RegisteredWithUNOS { get; set; }
        public bool RegisteredWithNCDR { get; set; }
        public bool RegisteredWithSVS { get; set; }
        public bool RegisteredWithELSO { get; set; }
        public bool RegisteredWithSSR { get; set; }
        public bool? UserConfirmed { get; set; }
        public DateTime? UserConfirmedDateUtc { get; set; }
    }
}
