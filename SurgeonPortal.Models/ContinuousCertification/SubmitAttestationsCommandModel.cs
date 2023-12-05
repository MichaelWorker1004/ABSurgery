using System;
using System.ComponentModel.DataAnnotations;

namespace SurgeonPortal.Models.ContinuousCertification
{
    public class SubmitAttestationsCommandModel
    {
        public int UserId { get; set; }
        public DateTime SigReceive { get; set; }
        public DateTime CertnoticeReceive { get; set; }
    }
}
