namespace Billing.Api.Models
{
    public class OrderRequest
    {
        public string OrderNumber { get; set; } = string.Empty;
        public int UserId { get; set; }
        public decimal PayableAmount { get; set; }
        public PaymentGatewayEnum PaymentGateway { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
