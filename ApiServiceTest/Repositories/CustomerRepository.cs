using ApiServiceTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiServiceTest.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ApiServiceTest.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly TestApiDBEntities _context;

        public CustomerRepository(TestApiDBEntities context)
        {
            _context = context;
        }

        public async Task UpdateVIPCustomersAsync()
        {
            var vipCustomerIds = await _context.Customers
                .Where(c => c.IsVip == 0) // Chỉ lấy khách hàng chưa là VIP
                .Select(c => new
                {
                    CustomerId = c.CustomerID,
                    TotalOrderAmount = c.Orders.Sum(o => o.TotalAmount),
                    DistinctProductCount = c.Orders
                                           .SelectMany(o => o.OrderItems)
                                           .Select(oi => oi.ProductID)
                                           .Distinct()
                                           .Count()
                })
                .Where(c => c.TotalOrderAmount > 1000000 || c.DistinctProductCount > 10)
                .Select(c => c.CustomerId)
                .ToListAsync();

            // Cập nhật trạng thái IsVip
            if (vipCustomerIds.Any())
            {
                var customersToUpdate = await _context.Customers
                    .Where(c => vipCustomerIds.Contains(c.CustomerID))
                    .ToListAsync();

                foreach (var customer in customersToUpdate)
                {
                    customer.IsVip = 1;
                }

                await _context.SaveChangesAsync();
            }
        }


    }
}