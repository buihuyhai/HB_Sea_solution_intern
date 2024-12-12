using ApiServiceTest.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

public class CustomerServices
{
    private readonly TestApiDBEntities _context;

    public CustomerServices(TestApiDBEntities context)
    {
        _context = context;
    }

    public async Task UpdateVIPCustomersAsync()
    {
        var customersToUpdate = await _context.Customers
            .Where(c => c.Orders.Any() &&
                        (c.Orders.Sum(o => o.TotalAmount) > 1000000 ||
                         c.Orders.SelectMany(o => o.OrderItems).Select(oi => oi.ProductID).Distinct().Count() > 10))
            .ToListAsync();

        foreach (var customer in customersToUpdate)
        {
            customer.IsVip =1;
        }

        await _context.SaveChangesAsync();
    }
}
