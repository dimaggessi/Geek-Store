using GeekStore.Domain.Entities.OrderAggregate;

namespace GeekStore.Domain.Specifications
{
    public class OrderSpecification : Specification<Order>
    {
        public OrderSpecification(string email) : base(x => x.CustomerEmail == email)
        {
            AddInclude(x => x.OrderItems);
            AddOrderByDescending(x => x.OrderDate);
        }

        public OrderSpecification(string email, int orderId) 
            : base(x => x.CustomerEmail == email && x.Id == orderId) 
        {
            AddInclude(x => x.OrderItems);
            AddOrderByDescending(x => x.OrderDate);
        }

        public OrderSpecification(string paymentIntentId, bool isPaymentIntent): 
            base(x => x.PaymentIntentId == paymentIntentId)
        {
            AddInclude(x => x.OrderItems);
            AddOrderByDescending(x => x.OrderDate);
        }
    }
}
