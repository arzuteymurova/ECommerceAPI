using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.DTOs.Order;

namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrder createOrder);
        Task<ListOrder> GetAllOrdersAsync(int page, int size);
        Task<SingleOrder> GetOrderByIdAsync(Guid id);
        Task CompleteOrder(Guid orderId);
    }
}
