using System;

namespace SurgeonPortal.Models.Billing
{
    public class ApplicationFeeReadOnlyModel
    {
        public decimal? SubTotal { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal? PaidTotal { get; set; }
        public decimal? BalanceDue { get; set; }
        public string TrackCode { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
