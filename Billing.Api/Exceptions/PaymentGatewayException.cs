namespace Billing.Api.Exceptions
{
    public class PaymentGatewayException : Exception
    {
        public PaymentGatewayException(string message) : base(message)
        {

        }
    }
}
