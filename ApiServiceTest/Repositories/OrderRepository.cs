using ApiServiceTest.Interfaces;
using ApiServiceTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web;

namespace ApiServiceTest.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TestApiDBEntities context;
        public OrderRepository(TestApiDBEntities contextt)
        {
            context = contextt;
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await context.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.OrderItems.Select(oi => oi.Product.Shop))
                .ToListAsync();
        }

        public async Task<Order> GetOrderDetailsAsync(string orderId)
        {
            return await context.Orders
                .Where(o => o.OrderID == orderId)
                .Include(o => o.OrderItems.Select(oi => oi.Product.Shop))
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CreateOrderAsync(Order order)
        {
            if (order == null || order.OrderItems == null || !order.OrderItems.Any())
                return false;

            context.Orders.Add(order);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveOrderItemAsync(string orderId, string productId)
        {
            var orderItem = await context.OrderItems
                .FirstOrDefaultAsync(oi => oi.OrderID == orderId && oi.ProductID == productId);

            if (orderItem == null)
                return false;

            context.OrderItems.Remove(orderItem);

            var order = await context.Orders.FirstOrDefaultAsync(o => o.OrderID == orderId);
            if (order != null)
                order.TotalAmount -= (orderItem.Quantity ?? 0) * (orderItem.Price ?? 0);

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Order>> SearchOrdersByKeywordAsync(string keyword)
        {
            return await context.Orders
                .Include(o => o.OrderItems.Select(oi => oi.Product.Shop))
                .Where(o => o.OrderItems.Any(oi => oi.Product.Name.Contains(keyword)))
                .ToListAsync();
        }
    }
}
