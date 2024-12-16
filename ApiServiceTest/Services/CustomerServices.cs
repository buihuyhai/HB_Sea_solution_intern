using ApiServiceTest.Models;
using ApiServiceTest.UnitOfWorks;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

public class CustomerServices
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerServices(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task UpdateVIPCustomersAsync()
    {
        await _unitOfWork.CustomerRepository.UpdateVIPCustomersAsync();
    }
}

