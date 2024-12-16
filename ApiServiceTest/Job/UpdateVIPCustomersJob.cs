using ApiServiceTest.UnitOfWorks;
using Quartz;
using log4net;
using System;
using System.Threading.Tasks;

public class UpdateVIPCustomersJob : IJob
{
    private readonly CustomerServices _customerServices;
    private static readonly ILog _logger = LogManager.GetLogger(typeof(UpdateVIPCustomersJob));

    public UpdateVIPCustomersJob(CustomerServices customerServices)
    {
        _customerServices = customerServices;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            await _customerServices.UpdateVIPCustomersAsync();
            _logger.Info("VIP customer update completed successfully.");
        }
        catch (Exception ex)
        {
            _logger.Error("Error while updating VIP customers.", ex);
        }
    }
}
