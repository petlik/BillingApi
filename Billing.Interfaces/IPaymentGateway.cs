namespace Billing.Interfaces
{
    public interface IPaymentGateway
    {
        Task<bool> RequestPayment(string orderNumber, decimal amount);
    }
}
