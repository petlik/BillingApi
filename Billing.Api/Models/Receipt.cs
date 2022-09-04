namespace Billing.Api.Models
{
    public class Receipt
    {
        public Guid Guid { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public decimal PayableAmount { get; set; }
    }
}
