using ApiServiceTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ApiServiceTest.Interfaces;
using ApiServiceTest.UnitOfWorks;




namespace ApiServiceTest.Services
{
    public class OrderServices
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await unitOfWork.OrderRepository.GetOrdersAsync();
        }

        public async Task<Order> GetOrderDetailsAsync(string orderId)
        {
            return await unitOfWork.OrderRepository.GetOrderDetailsAsync(orderId);
        }

        public async Task<bool> CreateOrderAsync(Order order)
        {
            return await unitOfWork.OrderRepository.CreateOrderAsync(order);
        }

        public async Task<bool> RemoveOrderItemAsync(string orderId, string productId)
        {
            return await unitOfWork.OrderRepository.RemoveOrderItemAsync(orderId, productId);
        }

        public async Task<List<Order>> SearchOrdersByKeywordAsync(string keyword)
        {
            return await unitOfWork.OrderRepository.SearchOrdersByKeywordAsync(keyword);
        }
    }
}
