using System.Collections.Generic;
using System.Threading.Tasks;
using ApiServiceTest.Models;

namespace ApiServiceTest.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrdersAsync();
        Task<Order> GetOrderDetailsAsync(string orderId);
        Task<bool> CreateOrderAsync(Order order);
        Task<bool> RemoveOrderItemAsync(string orderId, string productId);
        Task<List<Order>> SearchOrdersByKeywordAsync(string keyword);
    }
}
