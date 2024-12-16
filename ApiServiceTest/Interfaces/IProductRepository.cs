using System.Collections.Generic;
using System.Threading.Tasks;
using ApiServiceTest.Models;

namespace ApiServiceTest.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(string productId);
        Task<bool> CreateProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(string productId);
    }
}
