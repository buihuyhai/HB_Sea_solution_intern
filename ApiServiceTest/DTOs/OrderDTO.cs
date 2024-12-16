using ApiServiceTest.EntityRespones;
using ApiServiceTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiServiceTest.OrderDTO
{
    public static class OrderMapper
    {
        public static OrderResponse ToOrderResponse(Order order)
        {
            return new OrderResponse
            {
                OrderID = order.OrderID,
                ItemCount = order.OrderItems.Sum(oi => oi.Quantity ?? 0),
                TotalAmount = order.TotalAmount,
                ShopName = order.OrderItems.Select(oi => oi.Product?.Shop?.Name).FirstOrDefault() ?? "N/A",
                EstimatedDeliveryDate = order.OverdueDate ?? DateTime.MinValue,
                DeliveryStatus = order.DeliveryStatus
            };
        }

        public static OrderDetailResponse ToOrderDetailResponse(Order order)
        {
            return new OrderDetailResponse
            {
                OrderID = order.OrderID.ToString(),
                ItemCount = order.OrderItems.Sum(oi => oi.Quantity ?? 0),
                TotalAmount = order.TotalAmount,
                ShopName = order.OrderItems.Select(oi => oi.Product?.Shop?.Name).FirstOrDefault() ?? "N/A",
                EstimatedDeliveryDate = order.OverdueDate ?? DateTime.MinValue,
                DeliveryStatus = order.DeliveryStatus,
                OrderItems = order.OrderItems.Select(oi => new OrderItemResponse
                {
                    ProductName = oi.Product?.Name ?? "Unknown Product",
                    Quantity = oi.Quantity ?? 0,
                }).ToList()
            };
        }

        public static List<OrderResponse> ToOrderResponseList(IEnumerable<Order> orders)
        {
            return orders.Select(ToOrderResponse).ToList();
        }
    }
}
