using GeekStore.Domain.Entities;

namespace GeekStore.Domain.Interfaces
{
    public interface IDeliveryService
    {
        Task<List<DeliveryMethod>> DeliveryMethods(List<Product> products, string postalCode, int? id = default);
    }
}