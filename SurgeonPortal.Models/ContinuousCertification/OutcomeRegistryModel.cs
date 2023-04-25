using System;
using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.ContinuousCertification
{
    public class OutcomeRegistryModel
    {
        public bool SurgeonSpecificRegistry { get; set; }
        public string RegistryComments { get; set; }
        public bool RegisteredWithACHQC { get; set; }
        public bool RegisteredWithCESQIP { get; set; }
        public bool RegisteredWithMBSAQIP { get; set; }
        public bool RegisteredWithABA { get; set; }
        public bool RegisteredWithASBS { get; set; }
        public bool RegisteredWithStatewideCollaboratives { get; set; }
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
        public bool UserConfirmed { get; set; }
        public DateTime UserConfirmedDateUtc { get; set; }
        public int UserId { get; set; }
    }
}
