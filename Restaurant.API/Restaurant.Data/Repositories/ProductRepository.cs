using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Restaurant.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly RestaurantDbContext _dbContext;

        public ProductRepository(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddProductAsync(Product product)
        {
            await _dbContext.AddAsync(product);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product product)
        {
            _dbContext.Remove(product);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync(
            PaggingParameters? paggingParameters,
            SortDirection? isDesending = SortDirection.Ascending,
            List<Expression<Func<Product, bool>>> filters = null,
            Expression<Func<Product, object>> orderBy = null)
        {
            var query = _dbContext.Products
                    .Include(x => x.Category)
                    .AsQueryable();

            filters?.ForEach(x => query = query.Where(x));

            if (orderBy != null)
            {
                query = (isDesending == SortDirection.Descending) ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
            }

            var result = new List<Product>();

            if (paggingParameters != null)
            {
                 result = await query
                .Skip((paggingParameters.PageNumber - 1) * paggingParameters.PageSize)
                .Take(paggingParameters.PageSize)
                .ToListAsync();
            }
            else
            {
                result = await query.ToListAsync();
            }

            return result;
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            var product = await _dbContext.Products.Include(c => c.Category).FirstOrDefaultAsync(p => p.Id == id);

            return product;
        }

        public async Task<Product> UpdateProductAsync(string id, Product product)
        {
            var existingProduct = await GetProductByIdAsync(id);

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.Description = product.Description;

            await _dbContext.SaveChangesAsync();

            return existingProduct;
        }
    }
}
