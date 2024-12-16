using ApiServiceTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ApiServiceTest.Interfaces
{
    public interface ICustomerRepository
    {
        Task UpdateVIPCustomersAsync();
    }
}