using System;
using Ytg.Framework.Csla;

namespace SurgeonPortal.Library.Contracts.ContinuousCertification
{
    public interface IOutcomeRegistry : IYtgBusinessBase
    {
        int Id { get; set; }
        int UserId { get; set; }
        string SurgeonSpecificRegistry { get; set; }
        string RegistryComments { get; set; }
        bool RegisteredWithACHQC { get; set; }
        bool RegisteredWithCESQIP { get; set; }
        bool RegisteredWithMBSAQIP { get; set; }
        bool RegisteredWithABA { get; set; }
        bool RegisteredWithASBS { get; set; }
        bool RegisteredWithMSQC { get; set; }
        bool RegisteredWithABMS { get; set; }
        bool RegisteredWithNCDB { get; set; }
        bool RegisteredWithRQRS { get; set; }
        bool RegisteredWithNSQIP { get; set; }
        bool RegisteredWithNTDB { get; set; }
        bool RegisteredWithSTS { get; set; }
        bool RegisteredWithTQIP { get; set; }
        bool RegisteredWithUNOS { get; set; }
        bool RegisteredWithNCDR { get; set; }
        bool RegisteredWithSVS { get; set; }
        bool RegisteredWithELSO { get; set; }
        bool RegisteredWithSSR { get; set; }
        bool? UserConfirmed { get; set; }
        DateTime? UserConfirmedDateUtc { get; set; }
    }
}
