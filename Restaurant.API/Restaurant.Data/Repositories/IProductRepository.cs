using Restaurant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Restaurant.Data.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync(
            PaggingParameters? paggingParameters = null,
            SortDirection? sortDirection = SortDirection.Ascending,
            List<Expression<Func<Product, bool>>> filters = null,
            Expression<Func<Product, object>> orderBy = null);

        Task AddProductAsync(Product product);

        Task<Product> GetProductByIdAsync(string id);

        Task<Product> UpdateProductAsync(string id, Product product);

        Task DeleteProductAsync(Product product);
    }
}
