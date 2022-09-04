using Billing.Api.Exceptions;
using Billing.Api.Models;

namespace Billing.Api.Services
{
    public interface IOrderService
    {
        Task<Receipt> ProcessOrder(OrderRequest order);
    }

    public class OrderService : IOrderService
    {
        private readonly IPaymentGatewaySelector _paymentGatewaySelector;
        private readonly IRecieptService _recieptService;

        public OrderService(IPaymentGatewaySelector paymentGatewaySelector,
            IRecieptService recieptService)
        {
            _paymentGatewaySelector = paymentGatewaySelector;
            _recieptService = recieptService;
        }

        public async Task<Receipt> ProcessOrder(OrderRequest order)
        {
            var paymentResult = await _paymentGatewaySelector
                .SelectPaymentGateway(order.PaymentGateway)
                .RequestPayment(order.OrderNumber, order.PayableAmount);

            if (!paymentResult)
                throw new PaymentGatewayException("Payment gateway failed to process order");

            return _recieptService.Generate(order);
        }
    }
}
