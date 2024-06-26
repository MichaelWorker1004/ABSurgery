﻿namespace SurgeonPortal.Shared.PaymentProvider
{
    public class PaymentProviderConfiguration
    {
        public string? Url { get; set; }
        public string? MerchantId { get; set; }
        public string? UserId { get; set; }
        public string? Pin { get; set; }
        public string ErrorEmailRecipient { get; set; }
        public Dictionary<string, string> EmailTemplateIds { get; set; }
    }
}
