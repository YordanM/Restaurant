using Restaurant.Data.Repositories;
using Restaurant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Restaurant.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository) 
        {
            _productRepository = productRepository;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var allProducts = await _productRepository.GetAllAsync();

            var isUnique = allProducts.Any(p => p.Name == product.Name);

            if (isUnique)
            {
                throw new Exception("Product name already exists.");
            }

            await _productRepository.AddProductAsync(product);

            return product;
        }

        public async Task DeleteProductAsync(string id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            await _productRepository.DeleteProductAsync(product);
        }

        public async Task<IEnumerable<Product>> GetAllAsync(
            PaggingParameters? paggingParameters = null,
            SortDirection? sortDirection = null,
            string? product = null,
            string? categoryId = null,
            string? sortBy = null)
        {
            List<Expression<Func<Product, bool>>> filters = new List<Expression<Func<Product, bool>>>();

            if (product != null)
            {
                filters.Add(x => x.Name.Contains(product));
            }
            if (categoryId != null)
            {
                filters.Add(x => x.CategoryId == categoryId);
            }

            Expression<Func<Product, object>> orderBy = null;

            if (sortBy != null)
            {
                if (sortBy.ToLower() == "name")
                {
                    orderBy = x => x.Name;
                }
                else if (sortBy.ToLower() == "category")
                {
                    orderBy = x => x.Category.Name;
                }
            }

            return await _productRepository.GetAllAsync(paggingParameters, sortDirection, filters, orderBy);
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task<Product> UpdateProductAsync(string id, Product product)
        {

            if (product == null)
            {
                throw new ArgumentNullException();
            }

            var products = await _productRepository.GetAllAsync();

            var isUnique = products.Any(p => p.Name == product.Name);

            var existingProduct = products.FirstOrDefault(p => p.Id == id);

            if (existingProduct == null)
            {
                throw new Exception("No such category exists.");
            }

            if (isUnique && existingProduct.Name != product.Name)
            {
                throw new Exception("Product name already exists.");
            }

            return await _productRepository.UpdateProductAsync(id, product);
        }
    }
}
