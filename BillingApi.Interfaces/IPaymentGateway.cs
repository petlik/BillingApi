namespace BillingApi.Interfaces
{
    public interface IPaymentGateway
    {
        Task<bool> RequestPayment(string orderNumber, decimal amount);
    }
}