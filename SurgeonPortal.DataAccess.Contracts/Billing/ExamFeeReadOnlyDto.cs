using System;

namespace SurgeonPortal.DataAccess.Contracts.Billing
{
    public class ExamFeeReadOnlyDto
    {
        public decimal? SubTotal { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal? PaidTotal { get; set; }
        public decimal? BalanceDue { get; set; }
        public string ExamCode { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
