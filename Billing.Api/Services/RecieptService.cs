using Billing.Api.Models;

namespace Billing.Api.Services
{
    public interface IRecieptService
    {
        Receipt Generate(OrderRequest order);
    }

    public class RecieptService : IRecieptService
    {
        public Receipt Generate(OrderRequest order)
        {
            return new Receipt
            {
                Guid = Guid.NewGuid(),
                OrderNumber = order.OrderNumber,
                PayableAmount = order.PayableAmount
            };
        }
    }
}
