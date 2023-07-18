using System;

namespace SurgeonPortal.Models.ContinuingMedicalEducation
{
    public class CmeAdjustmentReadOnlyModel
    {
        public decimal CmeId { get; set; }
        public int? UserId { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public decimal CreditsTotal { get; set; }
        public decimal? CreditsSA { get; set; }
        public string IssuedBy { get; set; }
        public DateTime CreditExpDate { get; set; }
    }
}
