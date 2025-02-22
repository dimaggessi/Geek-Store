using GeekStore.Domain.Entities.OrderAggregate;
using GeekStore.Domain.Specifications.SpecParams;

namespace GeekStore.Domain.Specifications
{
    public class OrderSpecification : Specification<Order>
    {
        public OrderSpecification(string email) : base(order => order.CustomerEmail == email)
        {
            AddInclude(order => order.OrderItems);
            AddOrderByDescending(order => order.OrderDate);
        }

        public OrderSpecification(string email, int orderId) 
            : base(order => order.CustomerEmail == email && order.Id == orderId) 
        {
            AddInclude(order => order.OrderItems);
            AddOrderByDescending(order => order.OrderDate);
        }

        public OrderSpecification(string paymentIntentId, bool isPaymentIntent): 
            base(order => order.PaymentIntentId == paymentIntentId)
        {
            AddInclude(order => order.OrderItems);
            AddOrderByDescending(order => order.OrderDate);
        }

        public OrderSpecification(OrderSpecParams specParams) : base(order => 
            string.IsNullOrEmpty(specParams.Status) || 
            order.OrderStatus == ParseStringToOrderStatus(specParams.Status))
        {
            var skip = specParams.PageSize * (specParams.PageIndex - 1);
            var take = specParams.PageSize;

            AddInclude(order => order.OrderItems);
            AddPagination(skip, take);
            AddOrderByDescending(order => order.OrderDate);
        }

        public OrderSpecification(int id) : base(order => order.Id == id)
        {
            AddInclude(order => order.OrderItems);
        }

        private static OrderStatus? ParseStringToOrderStatus(string status)
        {
            if (Enum.TryParse<OrderStatus>(value: status, ignoreCase: true, out var result))
            {
                return result;
            }
            return null;
        }
    }
}
