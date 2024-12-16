using ApiServiceTest.Interfaces;
using ApiServiceTest.Models;
using ApiServiceTest.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiServiceTest.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository OrderRepository { get; }
        ICustomerRepository CustomerRepository { get; }

        IProductRepository ProductRepository { get; }

        void SaveChanges();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private TestApiDBEntities context;
        private IOrderRepository orderRepository;
        private ICustomerRepository customerRepository;
        private IProductRepository productRepository;
        public UnitOfWork(TestApiDBEntities context)
        {
            this.context = context;
        }

        
        public IOrderRepository OrderRepository
        {
            get
            {
                if (orderRepository == null)
                {
                    orderRepository = new OrderRepository(context);
                }

                return orderRepository;
            }
        }
        public ICustomerRepository CustomerRepository
        {
            get
            {
                if (customerRepository == null)
                {
                    customerRepository = new CustomerRepository(context);
                }

                return customerRepository;
            }
        }
        public IProductRepository ProductRepository
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new ProductRepository(context);
                }

                return productRepository;
            }
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
        public void Dispose()
        {
            context.Dispose();
        }

    }
}