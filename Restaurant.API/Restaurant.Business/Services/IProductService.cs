using Restaurant.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Business.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync(
            PaggingParameters? paggingParameters,
            SortDirection? sortDirection,
            string? product = null,
            string? categoryId = null,
            string? sortBy = null);

        Task<Product> AddProductAsync(Product product);

        Task<Product> GetProductByIdAsync(string id);

        Task<Product> UpdateProductAsync(string id, Product product);

        Task DeleteProductAsync(string id);
    }
}
