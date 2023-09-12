using System;

namespace SurgeonPortal.Models.Billing
{
    public class ExamFeeReadOnlyModel
    {
        public decimal? SubTotal { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal? PaidTotal { get; set; }
        public decimal? BalanceDue { get; set; }
        public string ExamCode { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
