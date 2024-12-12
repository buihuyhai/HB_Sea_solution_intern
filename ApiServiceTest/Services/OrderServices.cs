using ApiServiceTest.EntityRespones;
using ApiServiceTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using ApiServiceTest.EntityRequests;

namespace ApiServiceTest.Services
{
    public class OrderServices
    {
        private readonly TestApiDBEntities _context;

        public OrderServices(TestApiDBEntities context)
        {
            _context = context;
        }

        public async Task<List<OrderResponse>> GetOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.Customer)
                .Include(o => o.ShippingProvider)
                .Include(o => o.PaymentMethod)
                .ToListAsync();

            var orderResponses = orders.Select(order => new OrderResponse
            {
                OrderID = order.OrderID,
                ItemCount = order.OrderItems.Sum(oi => oi.Quantity ?? 0),
                TotalAmount = order.TotalAmount,
                ShopName = order.OrderItems
                    .Select(oi => oi.Product?.Shop?.Name)
                    .FirstOrDefault() ?? "N/A",
                EstimatedDeliveryDate = order.OverdueDate ?? DateTime.MinValue,
                DeliveryStatus = order.DeliveryStatus
            }).ToList();

            return orderResponses;
        }

        public async Task<OrderDetailResponse> GetOrderDetailsAsync(string orderId)
        {
            var order = await _context.Orders
                .Where(o => o.OrderID.ToString() == orderId)
                .Include(o => o.OrderItems)
                .Include(o => o.Customer)
                .Include(o => o.ShippingProvider)
                .Include(o => o.PaymentMethod)
                .Include(o => o.OrderItems.Select(oi => oi.Product))
                .Include(o => o.OrderItems.Select(oi => oi.Product.Shop))
                .FirstOrDefaultAsync();

            if (order == null)
            {
                return null;
            }

            var orderDetailResponse = new OrderDetailResponse
            {
                OrderID = order.OrderID.ToString(),
                ItemCount = order.OrderItems.Sum(oi => oi.Quantity ?? 0),
                TotalAmount = order.TotalAmount,
                ShopName = order.OrderItems
                    .Select(oi => oi.Product?.Shop?.Name)
                    .FirstOrDefault() ?? "N/A",
                EstimatedDeliveryDate = order.OverdueDate ?? DateTime.MinValue,
                DeliveryStatus = order.DeliveryStatus,
                OrderItems = order.OrderItems.Select(oi => new OrderItemResponse
                {
                    ProductName = oi.Product?.Name ?? "Unknown Product",
                    Quantity = oi.Quantity ?? 0,
                }).ToList()
            };

            return orderDetailResponse;
        }

        public async Task<bool> CreateOrderAsync(CreateOrderRequest request)
        {

            var newOrder = new Order
            {
                OrderID = Guid.NewGuid().ToString(),
                OrderDate = DateTime.Now,
                CustomerID = request.CustomerID,
                ShippingProviderID = request.ShippingProviderID,
                PaymentMethodID = request.PaymentMethodID,
                TotalAmount = request.TotalAmount,
                DeliveryStatus = request.DeliveryStatus,
                OverdueDate = request.OverdueDate,
                PaymentStatus = request.PaymentStatus,
                PaidAt = request.PaidAt,
                OrderItems = request.OrderItems.Select(item => new OrderItem
                {
                    OrderID = Guid.NewGuid().ToString(),
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveOrderItemAsync(string orderId, string productId)
        {

            var orderItem = await _context.OrderItems
                .FirstOrDefaultAsync(oi => oi.OrderID == orderId && oi.ProductID == productId);

            if (orderItem == null)
            {
                return false;
            }

            _context.OrderItems.Remove(orderItem);

            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderID == orderId);
            if (order != null)
            {
                order.TotalAmount -= (orderItem.Quantity ?? 0) * (orderItem.Price ?? 0);
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<OrderResponse>> SearchOrdersByKeywordAsync(string keyword)
        {

            var orders = await _context.Orders
                .Include(o => o.OrderItems.Select(oi => oi.Product))
                .Where(o => o.OrderItems.Any(oi => oi.Product.Name.Contains(keyword)))
                .ToListAsync();

            var orderResponses = orders.Select(order => new OrderResponse
            {
                OrderID = order.OrderID,
                ItemCount = order.OrderItems.Sum(oi => oi.Quantity ?? 0),
                TotalAmount = order.TotalAmount,
                ShopName = order.OrderItems
                    .Select(oi => oi.Product?.Shop?.Name)
                    .FirstOrDefault() ?? "N/A",
                EstimatedDeliveryDate = order.OverdueDate ?? DateTime.MinValue,
                DeliveryStatus = order.DeliveryStatus
            }).ToList();

            return orderResponses;
        }


    }
}
