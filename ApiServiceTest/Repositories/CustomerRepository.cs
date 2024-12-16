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
            var customersToUpdate = await _context.Customers
                .Where(c => c.Orders.Any() &&
                            (c.Orders.Sum(o => o.TotalAmount) > 1000000 ||
                             c.Orders.SelectMany(o => o.OrderItems).Select(oi => oi.ProductID).Distinct().Count() > 10) &&
                            c.IsVip == 0)  //Chỉ update những khách hàng chưa là vip
                .ToListAsync();

            foreach (var customer in customersToUpdate)
            {
                customer.IsVip = 1;
            }

            await _context.SaveChangesAsync();
        }

    }
}