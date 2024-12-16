using ApiServiceTest.Interfaces;
using ApiServiceTest.Models;
using ApiServiceTest.Repositories;
using ApiServiceTest.Services;
using ApiServiceTest.UnitOfWorks;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace ApiServiceTest
{
    public static class UnityConfig
    {
        public static UnityContainer Container { get; private set; }

        public static void RegisterComponents()
        {
            Container = new UnityContainer();

            // Đăng ký các service và repository
            Container.RegisterType<TestApiDBEntities>();
            Container.RegisterType<IUnitOfWork, UnitOfWork>();
            Container.RegisterType<ICustomerRepository, CustomerRepository>();
            Container.RegisterType<IOrderRepository, OrderRepository>();
            Container.RegisterType<IProductRepository, ProductRepository>();
            Container.RegisterType<CustomerServices>();
            Container.RegisterType<OrderServices>();
            Container.RegisterType<UpdateVIPCustomersJob>();


            // Áp dụng cấu hình
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(Container);
        }
    }
}
