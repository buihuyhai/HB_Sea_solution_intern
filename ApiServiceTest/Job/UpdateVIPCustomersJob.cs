using ApiServiceTest.Models;
using Quartz;
using System;
using System.Threading.Tasks;

public class UpdateVIPCustomersJob : IJob
{
    private readonly CustomerServices _customerServices;

    public UpdateVIPCustomersJob()
    {
        _customerServices = new CustomerServices(new TestApiDBEntities());
    }

    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("Running UpdateVIPCustomersJob...");
        try
        {
            await _customerServices.UpdateVIPCustomersAsync();
            Console.WriteLine("VIP customer update completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while updating VIP customers: {ex.Message}");
        }
    }
}
